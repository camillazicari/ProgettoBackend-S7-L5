using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoBackend_S7_L5.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biglietto_AspNetUsers_UserId",
                table: "Biglietto");

            migrationBuilder.DropForeignKey(
                name: "FK_Biglietto_Evento_EventoId",
                table: "Biglietto");

            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Artista_ArtistaId",
                table: "Evento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Evento",
                table: "Evento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Biglietto",
                table: "Biglietto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artista",
                table: "Artista");

            migrationBuilder.RenameTable(
                name: "Evento",
                newName: "Eventi");

            migrationBuilder.RenameTable(
                name: "Biglietto",
                newName: "Biglietti");

            migrationBuilder.RenameTable(
                name: "Artista",
                newName: "Artisti");

            migrationBuilder.RenameIndex(
                name: "IX_Evento_ArtistaId",
                table: "Eventi",
                newName: "IX_Eventi_ArtistaId");

            migrationBuilder.RenameIndex(
                name: "IX_Biglietto_UserId",
                table: "Biglietti",
                newName: "IX_Biglietti_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Biglietto_EventoId",
                table: "Biglietti",
                newName: "IX_Biglietti_EventoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eventi",
                table: "Eventi",
                column: "EventoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Biglietti",
                table: "Biglietti",
                column: "BigliettoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artisti",
                table: "Artisti",
                column: "ArtistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biglietti_AspNetUsers_UserId",
                table: "Biglietti",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Biglietti_Eventi_EventoId",
                table: "Biglietti",
                column: "EventoId",
                principalTable: "Eventi",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventi_Artisti_ArtistaId",
                table: "Eventi",
                column: "ArtistaId",
                principalTable: "Artisti",
                principalColumn: "ArtistaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biglietti_AspNetUsers_UserId",
                table: "Biglietti");

            migrationBuilder.DropForeignKey(
                name: "FK_Biglietti_Eventi_EventoId",
                table: "Biglietti");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventi_Artisti_ArtistaId",
                table: "Eventi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eventi",
                table: "Eventi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Biglietti",
                table: "Biglietti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artisti",
                table: "Artisti");

            migrationBuilder.RenameTable(
                name: "Eventi",
                newName: "Evento");

            migrationBuilder.RenameTable(
                name: "Biglietti",
                newName: "Biglietto");

            migrationBuilder.RenameTable(
                name: "Artisti",
                newName: "Artista");

            migrationBuilder.RenameIndex(
                name: "IX_Eventi_ArtistaId",
                table: "Evento",
                newName: "IX_Evento_ArtistaId");

            migrationBuilder.RenameIndex(
                name: "IX_Biglietti_UserId",
                table: "Biglietto",
                newName: "IX_Biglietto_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Biglietti_EventoId",
                table: "Biglietto",
                newName: "IX_Biglietto_EventoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Evento",
                table: "Evento",
                column: "EventoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Biglietto",
                table: "Biglietto",
                column: "BigliettoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artista",
                table: "Artista",
                column: "ArtistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biglietto_AspNetUsers_UserId",
                table: "Biglietto",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Biglietto_Evento_EventoId",
                table: "Biglietto",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Artista_ArtistaId",
                table: "Evento",
                column: "ArtistaId",
                principalTable: "Artista",
                principalColumn: "ArtistaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
