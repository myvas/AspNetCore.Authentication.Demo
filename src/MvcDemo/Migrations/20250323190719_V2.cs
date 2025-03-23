using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
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
                name: "CreateUnixTime",
                table: "WeixinSubscribers");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "WeixinSubscribers");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "WeixinSubscribers",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "WeixinSubscribers",
                newName: "Nickname");

            migrationBuilder.RenameColumn(
                name: "AvatorImageUrl",
                table: "WeixinSubscribers",
                newName: "HeadImgUrl");
                
            migrationBuilder.RenameColumn(
                name: "SubscribedTime",
                table: "WeixinSubscribers",
                newName: "SubscribeTime");

            migrationBuilder.RenameColumn(
                name: "Unsubscribed",
                table: "WeixinSubscribers",
                newName: "Subscribed");
                
            migrationBuilder.RenameColumn(
                name: "UnsubscribedTime",
                table: "WeixinSubscribers",
                newName: "UnsubscribeTime");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "WeixinSubscribers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "WeixinSubscribers",
                type: "TEXT",
                nullable: true);
                
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "WeixinSubscribers",
                type: "TEXT",
                nullable: true);
                
            migrationBuilder.AddColumn<long>(
                name: "CreateTime",
                table: "WeixinReceivedMessages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "WeixinReceivedMessages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreateTime",
                table: "WeixinReceivedEvents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
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

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "WeixinSubscribers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "SubscribeTime",
                table: "WeixinSubscribers",
                newName: "SubscribedTime");

            migrationBuilder.RenameColumn(
                name: "UnsubscribeTime",
                table: "WeixinSubscribers",
                newName: "UnsubscribedTime");

            migrationBuilder.RenameColumn(
                name: "Subscribed",
                table: "WeixinSubscribers",
                newName: "Unsubscribed");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "WeixinSubscribers");

            migrationBuilder.RenameColumn(
                name: "HeadImgUrl",
                table: "WeixinSubscribers",
                newName: "AvatorImageUrl");

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

            migrationBuilder.AddColumn<long>(
                name: "CreateUnixTime",
                table: "WeixinReceivedMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

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
