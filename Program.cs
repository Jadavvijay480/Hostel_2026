using Hostel_2026.Data;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<RoomRepository>();
builder.Services.AddScoped<FeesRepository>();
builder.Services.AddScoped<WardenRepository>();
builder.Services.AddScoped<VisitorRepository>();
builder.Services.AddScoped<FeesViewRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<UserViewRepository>();
builder.Services.AddScoped<ComplaintRepository>();
builder.Services.AddScoped<StudentAttendanceRepository>();
builder.Services.AddScoped<RoomBookingRepository>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
