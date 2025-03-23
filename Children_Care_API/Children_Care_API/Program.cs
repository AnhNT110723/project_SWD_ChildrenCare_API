using Children_Care_API.Configurations;

var builder = WebApplication.CreateBuilder(args);


// Cấu hình appsettings
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Thêm dịch vụ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Cho phép nguồn gốc từ React
               .AllowAnyMethod()                     // Cho phép tất cả HTTP methods (GET, POST, v.v.)
               .AllowAnyHeader();                    // Cho phép tất cả headers
    });
});
//Nếu muốn cho phép tất cả nguồn gốc
//builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

// Cấu hình Database
builder.Services.AddDbContextConfiguration(builder.Configuration);

// Cấu hình DI
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Sử dụng CORS trước các middleware khác
app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
