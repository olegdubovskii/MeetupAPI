using MeetupAPI.DAL.UnitOfWork.Abstractions;
using MeetupAPI.DAL.UnitOfWork;
using MeetupAPI.DAL;
using MeetupAPI.PresentationLayer.Extensions;
using MeetupAPI.PresentationLayer.Middlewares.ExceptionMiddleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.InjectBusinessLogicLayer();
builder.Services.InjectDataAccessLayer(builder.Configuration);
builder.Services.InjectAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.InjectSwagger();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
