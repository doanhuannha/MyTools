/*
{now:yyyy/MM/dd}-NhaDoan:
	+ created
Sample:
	[sp_{table_name}_Update]
<all(,[n]):		@{col_name} = ?>
*/
CREATE PROC [sp_{table_name}_Update]
<all(,[n]):	@{col_name} {col_type}>
AS
UPDATE [{table_name}]
SET 
<nor(,[n]):	[{col_name}] = @{col_name}>
WHERE 
<pri([n]	AND):	[{col_name}] = @{col_name}> 