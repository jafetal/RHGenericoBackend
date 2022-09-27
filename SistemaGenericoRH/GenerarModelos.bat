dotnet ef dbcontext scaffold "Server=.\SQLEXPRESS;Database=SistemaRH;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --no-build -o "Models" -f --context ModelContext --no-pluralize --no-onconfiguring
pause
