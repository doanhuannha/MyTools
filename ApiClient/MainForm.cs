using BlueMoon.Common;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BlueMoon.ClientApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        ApiSettings _apiSettings = null;
        bool _loaded = false;
        
        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = this.Text.Trim() + " - v" + Assembly.GetEntryAssembly().GetName().Version.ToString(3);
            LoadApiSettings("apis.json");
            //Process.Start(@"C:\Users\F3X8AW3\AppData\Local\insomnia\Insomnia.exe");
        }

        

        private void ctrlApi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loaded) UpdateSelectedAPI();
        }

        private void ctrlEnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loaded) UpdateSelectedEnv();
        }
        private void Submit_Click(object sender, EventArgs e)
        {
            if (!ValidateParamValues())
            {
                MessageBox.Show("Please enter all param values!");
                return;
            }
            var envInfo = (ApiEnviroment)ctrlEnv.SelectedItem;
            var apiInfo = (ApiInfo)ctrlApi.SelectedItem;
            PreviewForm form = new PreviewForm();
            string content = $"{apiInfo.Method} {envInfo.Root.TrimEnd('/')}{ApplyParamValues(apiInfo.Url)}";
            bool isValidJson = true;
            if (!apiInfo.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                //C:\Users\F3X8AW3\Downloads\big_payload.txt
                string payloadTpl = ctrlRequestData.Text.Trim();
                if (payloadTpl.StartsWith("file://"))
                {
                    string filePath = payloadTpl.Replace("file://", "");
                    if (File.Exists(filePath)) content += $"\r\n\r\nLoad from file {filePath}";
                    else content += "[Unable to load the file]";
                }
                else
                {
                    var payload = ApplyParamValues(payloadTpl);
                    if (apiInfo.ContentType.IndexOf("json") >= 0)
                    {
                        try
                        {
                            payload = JsonSerializer.Serialize(JsonSerializer.Deserialize<object>(payload), new JsonSerializerOptions { WriteIndented = true });
                        }
                        catch
                        {
                            isValidJson = false;
                            payload = "!!INVALID PARAMETER VALUES DETECTED!!\r\n!!INVALID PARAMETER VALUES DETECTED!!\r\n" + payload;
                        }
                    }
                    content += $"\r\n\r\n{payload}";
                }
            }
            var rr = form.ShowDialog(content, this, isValidJson);
            if (rr == DialogResult.OK)
            {
                if (envInfo.Name.Contains("PROD", StringComparison.OrdinalIgnoreCase))
                {
                    var r = MessageBox.Show(this, "Are you sure to run this in PROD?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    if (r == DialogResult.No) return;
                }
                _ = SingleRequestAPI(envInfo, apiInfo, GetParamValues());
            }
        }
        private async void MultipleRun_Click(object sender, EventArgs e)
        {
            //prepare sample
            string sample = @$"[
    {GetParamValues()},
    {GetParamValues()}
]";
            ParameterForm form = new ParameterForm();
            var parameters = form.ShowDialogX(this, sample);
            if (!string.IsNullOrEmpty(parameters))
            {
                var envInfo = (ApiEnviroment)ctrlEnv.SelectedItem;
                if (envInfo.Name.Contains("PROD", StringComparison.OrdinalIgnoreCase))
                {
                    var r = MessageBox.Show(this, "Are you sure to run this in PROD?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    if (r == DialogResult.No) return;
                }
                var apiInfo = (ApiInfo)ctrlApi.SelectedItem;
                var paramSets = JsonSerializer.Deserialize<Dictionary<string, object>[]>(parameters);
                foreach(var set in paramSets)
                {
                    await SingleRequestAPI(envInfo, apiInfo, JsonSerializer.Serialize(set));
                }
            }

        }
        private void ctrlGridParams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var r = grid.Rows[e.RowIndex];
                var cellButton = r.Cells[e.ColumnIndex];
                FileForm dialog = new FileForm();
                var result = dialog.ShowDialogX(this);
                if (result != default)
                {
                    var fileInfo = new FileInfo(result.filePath);
                    cellButton.Tag = (result.mode, result.filePath);
                    var cellVal = r.Cells[1];
                    cellVal.Value = $"[file:{fileInfo.Name}]";
                }
                
                //dialog.SafeFileName
            }
        }
        private void LoadApiSettings(string settingsFile)
        {
            _apiSettings = JsonSerializer.Deserialize<ApiSettings>(settingsFile.ReadSecuredFile(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }); ;
            ctrlEnv.DisplayMember = "Name";
            ctrlApi.DisplayMember = "Name";

            ctrlEnv.DataSource = _apiSettings.Enviroments;

            UpdateSelectedEnv();
            UpdateSelectedAPI();
            _loaded = true;
        }
        //static Regex s_reg_Params = new Regex(@"<\?(?<k>[a-z]+\w*)(:(?<v>.*?))*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        static Regex s_reg_Params = new Regex(@"<\?(?<k>[a-z]+\w*)(:(?<v>.*?))?>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        void BuildParamValues(string data)
        {
            s_reg_Params.Matches(data).Select(m => (m.Groups["k"].Value, m.Groups["v"].Value)).DistinctBy(kp => 
            {
                (string k, string v) = kp;
                return k;
            }).All(kp =>
            {
                (string k, string v) = kp;
                ctrlGridParams.Rows.Add(k, v, "...");
                return true;
            });
                ;
            //
        }
        bool ValidateParamValues()
        {
            return ctrlGridParams.Rows.OfType<DataGridViewRow>().All(r => !string.IsNullOrEmpty(r.Cells[1].Value as string));
        }
        string GetParamValues()
        {
            var pp = ctrlGridParams.Rows.OfType<DataGridViewRow>()
                .Select(r => {
                    var val = (r.Cells[1].Value ?? "") as string;
                    if (val.StartsWith("[file:"))
                    {
                        var fileParam = ((string mode, string filePath))r.Cells[2].Tag;
                        
                        switch (fileParam.mode)
                        {
                            case "base64":
                                val = Convert.ToBase64String(File.ReadAllBytes(fileParam.filePath));
                                break;
                            default:
                                val = File.ReadAllText(fileParam.filePath);
                                break;
                        }
                    }
                    return $"\"{r.Cells[0].Value}\":\"{JsonEncodedText.Encode(val)}\"";
                }).ToArray();

            return "{" + string.Join(',', pp) + "}";

        }
        string ApplyParamValues(string data)
        {
            return ApplyParamValues(data, GetParamValues());
        }
        string ApplyParamValues(string data, string jsonParameters, int index = 1)
        {
            var apiInfo = (ApiInfo)ctrlApi.SelectedItem;
            if (string.IsNullOrEmpty(jsonParameters)) return data;
            var parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonParameters);
            parameters.All(kp =>
            {
                if (kp.Value != null)
                {
                    
                    string val = kp.Value.ToString();
                    if (kp.Value is JsonElement kind && (kind.ValueKind == JsonValueKind.True || kind.ValueKind == JsonValueKind.False))
                    {
                        val = val.ToLower();
                    }
                    if (kp.Value is bool) val = val.ToLower();
                    if (apiInfo.Parameters != null)
                    {
                        var extendParam = apiInfo.Parameters.FirstOrDefault(p => p.Name == kp.Key);
                        if (extendParam != null)
                        {
                            string tplFile = extendParam.Template;
                            if (File.Exists(tplFile))
                            {
                                var subTpl = File.ReadAllText(tplFile);
                                var paramSets = JsonSerializer.Deserialize<Dictionary<string, object>[]>(val);
                                string[] vals = new string[paramSets.Length];
                                for (int i = 0; i < paramSets.Length; i++)
                                {
                                    vals[i] = ApplyParamValues(subTpl, JsonSerializer.Serialize(paramSets[i]), i + 1);
                                }
                                val = string.Join(',', vals);
                            }
                        }
                    }
                    data = data.Replace($"[<{kp.Key}>]", $"[{val}]");
                    data = Regex.Replace(data, $@"\[\<\?{kp.Key}(:.*?)*\>\]", $"[{val}]", RegexOptions.Singleline);

                    data = data.Replace($"<{kp.Key}>", JsonEncodedText.Encode(val).ToString());
                    data = Regex.Replace(data, $@"\<\?{kp.Key}(:.*?)*\>", JsonEncodedText.Encode(val).ToString(), RegexOptions.Singleline);
                    data = data.Replace("<INDEX>", index.ToString());
                }
                data = data.Replace("<TODAY>", DateTime.Now.ToString("yyyy/MM/dd"));
                data = data.Replace("<NOW>", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                return true;
            });

            return data;

        }
        async Task<(string data, ApiResponse apiResponse)> RequestAPI(ApiEnviroment envInfo, ApiInfo apiInfo, string payloadTpl, string parameters, Action<(string requestUrl, string requestData)> onStart = null)
        {

            ApiClient client = new ApiClient(envInfo.Root, envInfo.Credential?.Locker, envInfo.Credential?.Key);
            string requestData = null, requestUrl = apiInfo.Url;
            requestUrl = ApplyParamValues(requestUrl, parameters);
            if (!apiInfo.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                if (payloadTpl.StartsWith("file://"))
                {
                    string filePath = payloadTpl.Replace("file://", "");
                    if (File.Exists(filePath)) payloadTpl = File.ReadAllText(filePath).Trim();
                }
                requestData = ApplyParamValues(payloadTpl, parameters);
            }
            if (onStart != null) onStart((requestUrl, requestData));
            return await client.Execute(requestUrl, requestData, apiInfo.ContentType, new HttpMethod(apiInfo.Method));
        }
        async Task SingleRequestAPI(ApiEnviroment envInfo, ApiInfo apiInfo, string paremters)
        {
            layoutMain.Enabled = false;
            ctrlResponseInfo.Text = "...";
            ctrlResponseData.Text = "";
            ctrlRequestData.ReadOnly = true;

            string payloadTpl = ctrlRequestData.Text.Trim();
            //check to load from file
            (string data, ApiResponse apiResponse) result = default;
            _ = Task.Run(async () =>
            {
                int cnt = 0;
                while (result == default)
                {
                    await Task.Delay(1000);
                    Invoke(() =>
                    {
                        ctrlResponseData.Text = $"Requesting... {++cnt}(s)";
                    });
                }
                Invoke(() =>
                {
                    ctrlResponseData.Text = result.data;
                });

            });
            result = await RequestAPI(envInfo, apiInfo, payloadTpl, paremters, (arg) => {
                ctrlResponseInfo.Text = $"{apiInfo.Method} {envInfo.Root}{arg.requestUrl}";
            });
            ctrlRequestData.ReadOnly = false;
            var response = result.apiResponse;
            string elapsedTime = response.ElapsedTime < 0 ? "Timeout" : response.ElapsedTime < 10000 ? $"{response.ElapsedTime.ToString("0.##")}(ms)" : $"{(response.ElapsedTime / 1000).ToString("0.##")}(s)";
            ctrlResponseInfo.Text += $" => HTTP {response.HttpCode} - Elapsed time: {elapsedTime}";
            layoutMain.Enabled = true;
        }
        private void UpdateSelectedEnv()
        {
            var envInfo = (ApiEnviroment)ctrlEnv.SelectedItem;

            ctrlApi.DataSource = _apiSettings.Apis.Where(a => string.IsNullOrEmpty(envInfo.Group) ? string.IsNullOrEmpty(a.Group) : a.Group?.ToLower() == envInfo.Group.ToLower()).ToList();
            if (ctrlApi.Items.Count == 0)
            {
                ctrlURL.Text = "";
                ctrlMethod.Text = "";
                ctrlDesc.Text = "";
                ctrlGridParams.Rows.Clear();
                ctrlResponseInfo.Text = "";
                ctrlResponseData.Text = "";
                ctrlRequestData.Text = "";
                ctrlGridParams.Enabled = ctrlRequestData.Enabled = ctrlSubmit.Enabled = false;
            }
            else
            {
                ctrlGridParams.Enabled = ctrlSubmit.Enabled = true;
            }
        }
        private void UpdateSelectedAPI()
        {
            var envInfo = (ApiEnviroment)ctrlEnv.SelectedItem;
            var apiInfo = (ApiInfo)ctrlApi.SelectedItem;
            ctrlURL.Text = $"{envInfo.Root.TrimEnd('/')}{apiInfo.Url}";
            ctrlMethod.Text = $"{apiInfo.Method} {apiInfo.ContentType}";
            ctrlDesc.Text = apiInfo.Description;
            ctrlRequestData.Enabled = !apiInfo.Method.Equals("GET", StringComparison.OrdinalIgnoreCase);
            if (ctrlRequestData.Enabled && !string.IsNullOrEmpty(apiInfo.Template))
            {
                ctrlRequestData.Text = File.ReadAllText(apiInfo.Template);
                BuildParamValues(ctrlRequestData.Text);
                
            }
            else
            {
                ctrlRequestData.Text = "";
            }
            ctrlGridParams.Rows.Clear();
            BuildParamValues(apiInfo.Url + " " + ctrlRequestData.Text);
            ctrlResponseInfo.Text = "...";
            ctrlResponseData.Text = "";
        }

        
    }
}