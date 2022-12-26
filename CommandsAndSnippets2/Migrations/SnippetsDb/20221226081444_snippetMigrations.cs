using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommandsAndSnippets2.Migrations.SnippetsDb
{
    /// <inheritdoc />
    public partial class snippetMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Snippets_SnippetId",
                table: "Content");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Content",
                table: "Content");

            migrationBuilder.RenameTable(
                name: "Content",
                newName: "Contents");

            migrationBuilder.RenameIndex(
                name: "IX_Content_SnippetId",
                table: "Contents",
                newName: "IX_Contents_SnippetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Snippets_SnippetId",
                table: "Contents",
                column: "SnippetId",
                principalTable: "Snippets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Snippets_SnippetId",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.RenameTable(
                name: "Contents",
                newName: "Content");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_SnippetId",
                table: "Content",
                newName: "IX_Content_SnippetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Content",
                table: "Content",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Snippets_SnippetId",
                table: "Content",
                column: "SnippetId",
                principalTable: "Snippets",
                principalColumn: "Id");
        }
    }
}
