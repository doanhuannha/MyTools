/*
{now:yyyy/MM/dd}-NhaDoan:
	+ created
Sample:
	[sp_{table_name}_Delete] <pri(, ):@{col_name} = ?>
*/
CREATE PROC [sp_{table_name}_Delete]
<pri(,[n]):	@{col_name} {col_type}>
AS
DELETE FROM [{table_name}]
WHERE 
<pri([n]	AND):	[{col_name}] = @{col_name}> 