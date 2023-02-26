using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopASPNet.Migrations
{
    /// <inheritdoc />
    public partial class Itemnowcontainslastediteddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Items",
                newName: "Created");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdited",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "LastEdited",
                table: "Items",
                newName: "CreationTime");
        }
    }
}
