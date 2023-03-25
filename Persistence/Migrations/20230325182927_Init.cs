using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentDataConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVisable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDataConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StudentDataConfigurationId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentData_StudentDataConfiguration_StudentDataConfigurationId",
                        column: x => x.StudentDataConfigurationId,
                        principalTable: "StudentDataConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentData_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentClass_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentClass_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Math 1" });

            migrationBuilder.InsertData(
                table: "StudentDataConfiguration",
                columns: new[] { "Id", "FieldName", "IsVisable" },
                values: new object[,]
                {
                    { 1, "First Name", true },
                    { 2, "Last Name", true },
                    { 3, "Email", true }
                });

            migrationBuilder.InsertData(
                table: "Students",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "CourseId", "Name" },
                values: new object[] { 1, 1, "Class A" });

            migrationBuilder.InsertData(
                table: "StudentData",
                columns: new[] { "Id", "StudentDataConfigurationId", "StudentId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, "john" },
                    { 2, 2, 1, "Doe" },
                    { 3, 3, 1, "John.Doe@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "StudentClass",
                columns: new[] { "Id", "ClassId", "StudentId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CourseId",
                table: "Classes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_ClassId",
                table: "StudentClass",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentClass_StudentId",
                table: "StudentClass",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentData_StudentDataConfigurationId",
                table: "StudentData",
                column: "StudentDataConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentData_StudentId",
                table: "StudentData",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentClass");

            migrationBuilder.DropTable(
                name: "StudentData");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "StudentDataConfiguration");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
