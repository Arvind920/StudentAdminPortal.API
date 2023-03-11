using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
////builder.Services.AddCors (options) =>
//{
//    options.AddPolicy("angularApplication", (builder) =>
//    {
//        builder.WithOrigin("http://localhost:4200/");
//    })
//};
builder.Services.AddControllers();
builder.Services.AddDbContext<StudentAdminContext>(options => options.UseSqlServer("server=LAPTOP-S3S24I8M\\SQLEXPRESS; database=StudentAdminPortalDb; Trusted_Connection=true; TrustServerCertificate=True"));
builder.Services.AddScoped<IStudentRepository, SqlStudentRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
