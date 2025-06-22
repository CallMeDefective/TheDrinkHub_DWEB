using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheDrinkHub_DWEB.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirProdutoIdItensEncomenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomendas_AspNetUsers_UtilizadorId",
                table: "Encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensEncomenda_Produtos_ProdutoId",
                table: "ItensEncomenda");

            migrationBuilder.AddColumn<Guid>(
                name: "ProdutoId1",
                table: "ItensEncomenda",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensEncomenda_ProdutoId1",
                table: "ItensEncomenda",
                column: "ProdutoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomendas_AspNetUsers_UtilizadorId",
                table: "Encomendas",
                column: "UtilizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensEncomenda_Produtos_ProdutoId",
                table: "ItensEncomenda",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensEncomenda_Produtos_ProdutoId1",
                table: "ItensEncomenda",
                column: "ProdutoId1",
                principalTable: "Produtos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomendas_AspNetUsers_UtilizadorId",
                table: "Encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensEncomenda_Produtos_ProdutoId",
                table: "ItensEncomenda");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensEncomenda_Produtos_ProdutoId1",
                table: "ItensEncomenda");

            migrationBuilder.DropIndex(
                name: "IX_ItensEncomenda_ProdutoId1",
                table: "ItensEncomenda");

            migrationBuilder.DropColumn(
                name: "ProdutoId1",
                table: "ItensEncomenda");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomendas_AspNetUsers_UtilizadorId",
                table: "Encomendas",
                column: "UtilizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensEncomenda_Produtos_ProdutoId",
                table: "ItensEncomenda",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
