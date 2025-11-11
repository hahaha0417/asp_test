using asp_base;
using asp_base.Controllers;
using asp_base.Models.test;
using AspNetCoreRateLimit;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using NLog.Web;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Profiling.SqlFormatters;
using System.Text;

// ------------------------------------------
// 初始化環境
// ------------------------------------------


hahaha.Initial_Environment();

ha.Log_Program!.LogDebug("初始化環境");
// ------------------------------------------
// 初始化
// ------------------------------------------
ha.Log_Program!.LogDebug("初始化");
hahaha.Initial();
// ------------------------------------------
// 初始化 UI
// ------------------------------------------
ha.Log_Program!.LogDebug("初始化UI");
hahaha.Initial_UI();

// ------------------------------------------
// 
// ------------------------------------------
// ------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 加入 EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMiniProfiler(options =>
{
    options.RouteBasePath = "/profiler"; // Customize the route for the profiler UI
    options.EnableMvcViewProfiling = true; // Enable profiling for MVC views
                                           // options.UserIdProvider = request => ConsistentUserId(request); // Optional: custom user ID provider
});

// 註冊 IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
// ------------------------------------------
// 
// ------------------------------------------
// 1. 設定 Hangfire 使用 SQL Server
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("Hangfire"), new SqlServerStorageOptions
          {
              CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
              QueuePollInterval = TimeSpan.Zero,
              UseRecommendedIsolationLevel = true,
              DisableGlobalLocks = true
          });

}); 


// 2. 啟動 Hangfire Server
builder.Services.AddHangfireServer(options =>
{
    options.ServerName = "EmailWorker";   // Worker 名稱
    //options.Queues = new[] { "default", "emails" };  // 指定監聽的 Queue
    options.WorkerCount = 2;              // 同時執行的 Job 數量
});
// ------------------------------------------
// 
// ------------------------------------------
//builder.Services.AddScoped<AdminFilter>();
//builder.Services.AddScoped<FrontendFilter>();

// 加入 Swagger 服務
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // 加入 JWT Bearer 安全定義
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "請輸入 JWT，格式為：Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    // 套用到所有 API 操作
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();


builder.Services.AddHttpClient();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

var app = builder.Build();
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();

// 轉發 Header 支援（必須在 UseRouting 前）

// ------------------------------------------
// 
// ------------------------------------------
// 3. 啟用 Dashboard
app.UseHangfireDashboard("/hangfire"); // 指定路徑
// ------------------------------------------
// 
// ------------------------------------------


hahaha.App_ = app;
ha.App = hahaha.App_;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiniProfiler();
// 測試時註解
//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
// 啟用 Swagger 中介層
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); // 確保路徑正確
    c.RoutePrefix = "docs"; // Swagger UI 的路徑
});

app.Run();


// ------------------------------------------
// 
// ------------------------------------------
ha.Log_Program!.LogDebug("關閉");
hahaha.Close();
// ------------------------------------------
// 
// ------------------------------------------