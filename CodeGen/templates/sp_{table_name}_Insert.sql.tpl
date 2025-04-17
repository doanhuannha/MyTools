/*
{now:yyyy/MM/dd}-NhaDoan:
	+ created
Sample:
	[sp_{table_name}_Insert]
<all(,[n]):		@{col_name} = ?>
*/
CREATE PROC [sp_{table_name}_Insert]
<all(,[n]):	@{col_name} {col_type}>
AS
INSERT [{table_name}](
<all(,[n]):	[{col_name}]>
)
VALUES (
<all(,[n]):	@{col_name}>
)