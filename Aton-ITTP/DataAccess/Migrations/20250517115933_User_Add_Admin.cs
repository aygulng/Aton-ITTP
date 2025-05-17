using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aton_ITTP.Migrations
{
    /// <inheritdoc />
    public partial class User_Add_Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""User"" (""Id"", ""Login"", ""Password"", ""Name"", ""Gender"", ""Admin"", ""CreatedBy"", ""ModifiedBy"")
                VALUES (gen_random_uuid(), 'Admin', 'Admin', 'Administrator', 1, true, 'System', 'System');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
