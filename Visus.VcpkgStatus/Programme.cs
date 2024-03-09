// <copyright file="Programme.cs" company="Visualisierungsinstitut der Universität Stuttgart">
// Copyright © 2024 Visualisierungsinstitut der Universität Stuttgart.
// Licenced under the MIT licence. See LICENCE.txt.
// </copyright>
// <author>Christoph Müller</author>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Visus.VcpkgStatus.Options;


var builder = WebApplication.CreateBuilder(args);

// Add structured options.
builder.Services.Configure<AppearenceOptions>(o => {
    builder.Configuration.GetSection(AppearenceOptions.SectionName).Bind(o);
});
builder.Services.Configure<CachingOptions>(o => {
    builder.Configuration.GetSection(CachingOptions.SectionName).Bind(o);
});
builder.Services.Configure<RequestOptions>(o => {
    builder.Configuration.GetSection(RequestOptions.SectionName).Bind(o);
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient("PortClient")
    .AddStandardResilienceHandler();

var app = builder.Build();


// Configure PathBase. This must be first!
var pathBase = builder.Configuration["PathBase"];
if (!string.IsNullOrWhiteSpace(pathBase)) {
    app.UsePathBase(pathBase);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger(o => {
        o.RouteTemplate = "/_swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(o => {
        o.RoutePrefix = "_swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
