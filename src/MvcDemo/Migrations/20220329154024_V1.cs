using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeixinResponseMessages");

            migrationBuilder.DropTable(
                name: "WeixinSendMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeixinSubscribers",
                table: "WeixinSubscribers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WeixinSubscribers");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "WeixinReceivedEvents");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "WeixinSubscribers",
                newName: "Remark");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "WeixinSubscribers",
                newName: "AppId");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "WeixinReceivedMessages",
                newName: "Recognition");

            migrationBuilder.AlterColumn<string>(
                name: "OpenId",
                table: "WeixinSubscribers",
                type: "TEXT",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "WeixinSubscribers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "WeixinSubscribers",
                type: "BLOB",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ToUserName",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Scale",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "MsgType",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MsgId",
                table: "WeixinReceivedMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserName",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreateUnixTime",
                table: "WeixinReceivedMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "ToUserName",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MsgType",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromUserName",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Event",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreateUnixTime",
                table: "WeixinReceivedEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeixinSubscribers",
                table: "WeixinSubscribers",
                column: "OpenId");

            migrationBuilder.CreateTable(
                name: "AuditEntires",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TableName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KeyValue = table.Column<string>(type: "TEXT", nullable: true),
                    OldValue = table.Column<string>(type: "TEXT", nullable: true),
                    NewValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntires", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEntires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeixinSubscribers",
                table: "WeixinSubscribers");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WeixinSubscribers");

            migrationBuilder.DropColumn(
                name: "CreateUnixTime",
                table: "WeixinReceivedMessages");

            migrationBuilder.DropColumn(
                name: "CreateUnixTime",
                table: "WeixinReceivedEvents");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "WeixinSubscribers",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "AppId",
                table: "WeixinSubscribers",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "Recognition",
                table: "WeixinReceivedMessages",
                newName: "CreateTime");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "WeixinSubscribers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpenId",
                table: "WeixinSubscribers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "WeixinSubscribers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ToUserName",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<decimal>(
                name: "Scale",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MsgType",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "MsgId",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FromUserName",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "ToUserName",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "MsgType",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserName",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Event",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "CreateTime",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeixinSubscribers",
                table: "WeixinSubscribers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WeixinResponseMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Format = table.Column<string>(type: "TEXT", nullable: true),
                    FromUserName = table.Column<string>(type: "TEXT", nullable: true),
                    Label = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    MediaId = table.Column<string>(type: "TEXT", nullable: true),
                    MsgId = table.Column<string>(type: "TEXT", nullable: true),
                    MsgType = table.Column<string>(type: "TEXT", nullable: true),
                    PicUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Scale = table.Column<decimal>(type: "TEXT", nullable: false),
                    ThumbMediaId = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    ToUserName = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeixinResponseMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeixinSendMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Format = table.Column<string>(type: "TEXT", nullable: true),
                    FromUserName = table.Column<string>(type: "TEXT", nullable: true),
                    Label = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    MediaId = table.Column<string>(type: "TEXT", nullable: true),
                    MsgId = table.Column<string>(type: "TEXT", nullable: true),
                    MsgType = table.Column<string>(type: "TEXT", nullable: true),
                    PicUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Scale = table.Column<decimal>(type: "TEXT", nullable: false),
                    ThumbMediaId = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    ToUserName = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeixinSendMessages", x => x.Id);
                });
        }
    }
}
