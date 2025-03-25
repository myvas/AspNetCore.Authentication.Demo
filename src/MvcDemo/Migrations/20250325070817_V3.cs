using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEntires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeixinSubscribers",
                table: "WeixinSubscribers");

            migrationBuilder.DropColumn(
                name: "AppId",
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
                name: "UnsubscribedTime",
                table: "WeixinSubscribers",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "Unsubscribed",
                table: "WeixinSubscribers",
                newName: "Subscribed");

            migrationBuilder.RenameColumn(
                name: "SubscribedTime",
                table: "WeixinSubscribers",
                newName: "HeadImgUrl");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "WeixinSubscribers",
                newName: "UnsubscribeTime");

            migrationBuilder.RenameColumn(
                name: "AvatorImageUrl",
                table: "WeixinSubscribers",
                newName: "ConcurrencyStamp");

            migrationBuilder.AlterColumn<string>(
                name: "OpenId",
                table: "WeixinSubscribers",
                type: "TEXT",
                maxLength: 32,
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

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "WeixinSubscribers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SubscribeTime",
                table: "WeixinSubscribers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ToUserName",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "MsgType",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<long>(
                name: "MsgId",
                table: "WeixinReceivedMessages",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserName",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreateTime",
                table: "WeixinReceivedMessages",
                type: "INTEGER",
                nullable: true);

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
                name: "ConcurrencyStamp",
                table: "WeixinReceivedEvents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreateTime",
                table: "WeixinReceivedEvents",
                type: "INTEGER",
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
                    FromUserName = table.Column<string>(type: "TEXT", nullable: true),
                    ToUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<long>(type: "INTEGER", nullable: true),
                    MsgType = table.Column<string>(type: "TEXT", nullable: true),
                    MsgId = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    PicUrl = table.Column<string>(type: "TEXT", nullable: true),
                    MediaId = table.Column<string>(type: "TEXT", nullable: true),
                    Format = table.Column<string>(type: "TEXT", nullable: true),
                    ThumbMediaId = table.Column<string>(type: "TEXT", nullable: true),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Scale = table.Column<decimal>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RequestId = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
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
                    FromUserName = table.Column<string>(type: "TEXT", nullable: true),
                    ToUserName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<long>(type: "INTEGER", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    ScheduleTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LastRetCode = table.Column<int>(type: "INTEGER", nullable: true),
                    LastRetMsg = table.Column<string>(type: "TEXT", nullable: true),
                    RetryTimes = table.Column<int>(type: "INTEGER", nullable: false),
                    LastTried = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeixinSendMessages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Sex",
                table: "WeixinSubscribers");

            migrationBuilder.DropColumn(
                name: "SubscribeTime",
                table: "WeixinSubscribers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "WeixinReceivedMessages");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "WeixinReceivedMessages");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "WeixinReceivedEvents");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "WeixinReceivedEvents");

            migrationBuilder.RenameColumn(
                name: "UnsubscribeTime",
                table: "WeixinSubscribers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "Subscribed",
                table: "WeixinSubscribers",
                newName: "Unsubscribed");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "WeixinSubscribers",
                newName: "UnsubscribedTime");

            migrationBuilder.RenameColumn(
                name: "HeadImgUrl",
                table: "WeixinSubscribers",
                newName: "SubscribedTime");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "WeixinSubscribers",
                newName: "AvatorImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "OpenId",
                table: "WeixinSubscribers",
                type: "TEXT",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppId",
                table: "WeixinSubscribers",
                type: "TEXT",
                nullable: true);

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
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

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
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KeyValue = table.Column<string>(type: "TEXT", nullable: true),
                    NewValue = table.Column<string>(type: "TEXT", nullable: true),
                    OldValue = table.Column<string>(type: "TEXT", nullable: true),
                    TableName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntires", x => x.Id);
                });
        }
    }
}
