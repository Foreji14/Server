using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace story_maker.Migrations
{
    public partial class mtm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traits_Characters_CharacterId",
                table: "Traits");

            migrationBuilder.DropIndex(
                name: "IX_Traits_CharacterId",
                table: "Traits");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Traits");

            migrationBuilder.CreateTable(
                name: "CharacterTrait",
                columns: table => new
                {
                    CharactersCharacterId = table.Column<int>(type: "int", nullable: false),
                    TraitsTraitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTrait", x => new { x.CharactersCharacterId, x.TraitsTraitId });
                    table.ForeignKey(
                        name: "FK_CharacterTrait_Characters_CharactersCharacterId",
                        column: x => x.CharactersCharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterTrait_Traits_TraitsTraitId",
                        column: x => x.TraitsTraitId,
                        principalTable: "Traits",
                        principalColumn: "TraitId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterTrait_TraitsTraitId",
                table: "CharacterTrait",
                column: "TraitsTraitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterTrait");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "Traits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Traits_CharacterId",
                table: "Traits",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Traits_Characters_CharacterId",
                table: "Traits",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId");
        }
    }
}
