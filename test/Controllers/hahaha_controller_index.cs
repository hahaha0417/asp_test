using asp_base.Models.test;
using AutoMapper;
using Emgu.CV.Face;
using Emgu.CV.Features2D;
using Emgu.CV.Ocl;
using ha_def;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.CircuitBreaker;
using StackExchange.Profiling;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

//public class AdminFilter : IActionFilter
//{
//    public void OnActionExecuting(ActionExecutingContext context)
//    {
//        // 前置處理：Admin 身份驗證或攔截
//        var isAdmin = context.HttpContext.User.Identity.IsAuthenticated
//                      && context.HttpContext.User.IsInRole("Admin");

//        if (!isAdmin)
//        {
//            // 如果不是 Admin，直接回傳 403 或 Redirect
//            context.Result = new ForbidResult();
//        }
//    }

//    public void OnActionExecuted(ActionExecutedContext context)
//    {
//        // 後置處理：可以紀錄日誌等
//        Console.WriteLine("Admin Action Executed");
//    }
//}

//public class FrontendFilter : IActionFilter
//{
//    public void OnActionExecuting(ActionExecutingContext context)
//    {
//        // 前置處理：檢查前台訪問條件
//        Console.WriteLine("Frontend Action Executing");
//    }

//    public void OnActionExecuted(ActionExecutedContext context)
//    {
//        // 後置處理：紀錄日誌或其他操作
//        Console.WriteLine("Frontend Action Executed");
//    }
//}

namespace asp_base.Controllers
{

    public class EmailJob
    {


        public EmailJob()
        {

        }

        [AutomaticRetry(Attempts = 5, DelaysInSeconds = new int[] { 10, 30, 60 })]
        // 傳入資料而不是整個物件
        public void SendEmail(EmailData data)
        {
            ha.Log_Program!.LogInformation($"寄送 Email 給 {data.To}, 主題: {data.Subject}");

        }


    }

    public class EmailData
    {
        public string To { get; set; }
        public string Subject { get; set; }
    }



    //[ServiceFilter(typeof(AdminFilter))]
    [Route("/")]
    public class hahaha_controller_index : Controller
    {

        // GET: hahaha_index_controller
        [HttpGet("")]
        public ActionResult Index()
        {
            // 刪除資料庫
            //using (var scope = ha.App!.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    // 刪除資料庫
            //    context.Database.EnsureDeleted();
            //    // 建立資料庫
            //    context.Database.EnsureCreated();
            //    // 用所有尚未執行的 Migrations，以更新資料庫結構
            //    if (!context.Database.CanConnect())
            //    {
            //        context.Database.Migrate(); // 或 EnsureCreated()
            //    }
            //}

            //var users1 = new object[]
            //{
            //    new { Id = 1, Name = "Alice" },
            //    new { Id = 2, Name = "Bob" }
            //};

            //var users2 = new object[]
            //{
            //    new { Id = 1, Name = "Alice" },
            //    new { Id = 2, Name = "Bob" },
            //    new string[] { "Alice", "Bob", "Charlie" }
            //};

            //foreach (var item in users)
            //{
            //    switch (item)
            //    {
            //        case string[] names:
            //            Console.WriteLine("String array: " + string.Join(", ", names));
            //            break;
            //        case { } obj: // 匿名型別，沒明確型別
            //                      // 使用反射取值
            //            var idProp = obj.GetType().GetProperty("Id");
            //            var nameProp = obj.GetType().GetProperty("Name");
            //            if (idProp != null && nameProp != null)
            //            {
            //                var id = idProp.GetValue(obj);
            //                var name = nameProp.GetValue(obj);
            //                Console.WriteLine($"Id: {id}, Name: {name}");
            //            }
            //            break;
            //    }
            //}

            //var data = new EmailData { To = "test@example.com", Subject = "Hello Hangfire" };

            //// 免費版不能指定queue
            //// 排程 Job 到 emails queue
            //BackgroundJob.Enqueue<EmailJob>(
            //    job => job.SendEmail(data)
            //);

            //ViewData["Message"]
            //ViewBag.Message

            //// Controller 1
            //TempData["Notice"] = "操作成功";
            //return RedirectToAction("Index");

            //// Controller 2 / View
            //var msg = TempData["Notice"]; // 只可讀一次，讀了就消失


            //| 方法 | 用法範例 | 說明 |
            //| -------------------------- | ---------------------------------------- | --------------------------------- |
            //| `Html.Raw(string)`         | `@Html.Raw("<strong>Hello</strong>")`    | 將字串原樣輸出為 HTML，不做編碼 |
            //| `Html.Encode(string)`      | `@Html.Encode("<strong>Hello</strong>")` | 對字串做 HTML 編碼，安全輸出 |
            //| `Html.DisplayTextFor(...)` | `@Html.DisplayTextFor(m => m.Name)`      | 顯示 Model 屬性值，會自動編碼 |
            //| `Html.DisplayFor(...)`     | `@Html.DisplayFor(m => m.Name)`          | 顯示 Model 屬性，依 DisplayTemplate 格式化 |

            //| 方法 | 用法 | 說明 |
            //| ----------------------------- | -------------------------------------------------------- | ------------------------------------------ |
            //| `Html.BeginForm()`            | `@using(Html.BeginForm("Action", "Controller")) { ... }` | 生成 `< form >` 標籤，支援 action/ controller / method |
            //| `Html.EndForm()`              | -                                                        | 結束表單(通常不需要手動呼叫，BeginForm 使用 `using`)      |
            //| `Html.TextBoxFor()`           | `@Html.TextBoxFor(m => m.Name)`                          | 生成 `< input type = "text" >`，綁定 Model          |
            //| `Html.PasswordFor()`          | `@Html.PasswordFor(m => m.Password)`                     | 生成 `< input type = "password" >`               |
            //| `Html.HiddenFor()`            | `@Html.HiddenFor(m => m.Id)`                             | 隱藏欄位 |
            //| `Html.TextAreaFor()`          | `@Html.TextAreaFor(m => m.Description)`                  | 多行文字欄位 |
            //| `Html.DropDownListFor()`      | `@Html.DropDownListFor(m => m.Country, Model.Countries)` | 下拉選單 |
            //| `Html.CheckBoxFor()`          | `@Html.CheckBoxFor(m => m.IsActive)`                     | 勾選框 |
            //| `Html.RadioButtonFor()`       | `@Html.RadioButtonFor(m => m.Gender, "Male")`            | 單選按鈕 |
            //| `Html.LabelFor()`             | `@Html.LabelFor(m => m.Name)`                            | 對應欄位生成 `< label >`                           |
            //| `Html.ValidationMessageFor()` | `@Html.ValidationMessageFor(m => m.Name)`                | 顯示單一欄位驗證訊息 |
            //| `Html.ValidationSummary()`    | `@Html.ValidationSummary()`                              | 顯示所有驗證錯誤訊息 |

            //WebUtility.HtmlDecode
            //@using System.Net
            //@{
            //    var encoded = Html.Encode("<strong>Hello</strong>");
            //    var decoded = WebUtility.HtmlDecode(encoded);
            //}

            // 取得參數
            // get
            //var id = HttpContext.Request.Query["id"].ToString();
            //var name = HttpContext.Request.Query["name"].ToString();
            // post
            //var id = HttpContext.Request.Form["id"].ToString();
            //var name = HttpContext.Request.Form["name"].ToString();

            //var id = string.IsNullOrEmpty(HttpContext.Request.Form["id"])
            //? "0"
            //: HttpContext.Request.Form["id"].ToString();

            //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", myfile.FileName);
            //using var stream = new FileStream(path, FileMode.Create);
            //myfile.CopyTo(stream);

            //// 查ChatGPT
            //類型 方法  說明
            //JSON    Json(), Ok(), Content(..., "application/json")  回傳物件 JSON
            //View View()  回傳 Razor View
            //Partial View PartialView()   回傳部分 HTML
            //File File(), PhysicalFile()  回傳檔案下載
            //Redirect    Redirect(), RedirectToAction()  重導頁面
            //Status  BadRequest(), NotFound(), NoContent()   回傳 HTTP 狀態
            //Content Content()   回傳文字或 HTML
            //Empty EmptyResult()   不回傳內容

            //app.MapGet("/", (HttpContext context) =>
            //{
            //    var path = context.Request.Path;
            //    var method = context.Request.Method;
            //    var query = context.Request.Query["id"]; // GET 參數
            //    var headers = context.Request.Headers["User-Agent"];

            //    return $"Path: {path}, Method: {method}, Query id: {query}, User-Agent: {headers}";
            //});

            //hahaha.Cache_Memory_.Set("key", "value", TimeSpan.FromMinutes(5));
            //var v = hahaha.Cache_Memory_.Get<string>("key2");
            //// 嘗試取得資料
            //if (hahaha.Cache_Memory_.TryGetValue("key", out string name))
            //{
            //    Console.WriteLine($"找到快取：{name}");
            //}
            //else
            //{
            //    Console.WriteLine("找不到快取");
            //}

            //if (!_cache.TryGetValue("data", out string value))
            //{
            //    await _lock.WaitAsync();
            //    try
            //    {
            //        // 二次確認，防止重複載入
            //        if (!_cache.TryGetValue("data", out value))
            //        {
            //            value = await LoadDataAsync();
            //            _cache.Set("data", value, TimeSpan.FromMinutes(5));
            //        }
            //    }
            //    finally
            //    {
            //        _lock.Release();
            //    }
            //}

            var order = new Order
            {
                Id = 1001,
                CustomerName = "Tom",
                Items = new List<OrderItem>
                {
                    new() { ProductName = "iPhone", Price = 30000 },
                    new() { ProductName = "iPad", Price = 15000 }
                }
            };

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
            }, loggerFactory);
            var mapper = config.CreateMapper();

            // 執行映射
            OrderDto orderDto = mapper.Map<OrderDto>(order);

            // 簡單範例：手動標記區段
            using (MiniProfiler.Current.Step("首頁資料載入"))
            {
                // 模擬工作
                System.Threading.Thread.Sleep(500);
            }

            return View("~/Views/home/index.cshtml");
        }

        public class Order
        {
            public int Id { get; set; }
            public string CustomerName { get; set; }
            public List<OrderItem> Items { get; set; }
        }

        public class OrderItem
        {
            public string ProductName { get; set; }
            public decimal Price { get; set; }
        }

        public class OrderDto
        {
            public int Id { get; set; }
            public string CustomerName { get; set; }
            public List<OrderItemDto> Items { get; set; }
        }

        public class OrderItemDto
        {
            public string ProductName2 { get; set; }
            public decimal Price { get; set; }
        }

        public class OrderProfile : Profile
        {
            public OrderProfile()
            {
                // 單筆對應規則
                CreateMap<Order, OrderDto>();
                CreateMap<OrderItem, OrderItemDto>().ForMember(dest => dest.ProductName2,
                       opt => opt.MapFrom(src => src.ProductName));
            }
        }

        [HttpGet("test")]
        public ActionResult Test()
        {
            return View("~/Views/home/test.cshtml");
        }

        [HttpGet("test_id/{id?}")]
        public ActionResult Test_Id(int? id)
        {
            if (id.HasValue)
            {
                ViewData["Id"] = id.Value;
            }
            else
            {
                ViewData["Id"] = 0; // 或其他預設值
            }

            return View("~/Views/home/test_id.cshtml");
        }

        [HttpPost("test_post")]
        public IActionResult Test_Post()
        {
            //var id = HttpContext.Request.Query["id"].ToString();
            var id = HttpContext.Request.Form["id"].ToString();

            //var options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,       // 美化輸出
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // 小駝峰命名
            //};

            //string jsonPretty = JsonSerializer.Serialize(obj, options);
            /* 輸出：
            {
              "id": 2,
              "name": "Alice"
            }
            */

            //Redirect("https://example.com");

            return Json(new { received_id = id });

        }



        [HttpPost("upload")]
        public IActionResult Upload(IFormFile myfile)
        {
            if (myfile != null && myfile.Length > 0)
            {
                // 取得檔名
                var fileName = myfile.FileName;

                // 讀取檔案內容
                using var stream = new MemoryStream();
                myfile.CopyTo(stream);
                var bytes = stream.ToArray();

                return Content($"Uploaded file: {fileName}, size: {bytes.Length} bytes");
            }

            return Content("No file uploaded");
        }

        [HttpPost("upload/multiple")]
        public IActionResult UploadMultiple()
        {
            var files = HttpContext.Request.Form.Files;

            foreach (var file in files)
            {
                var name = file.FileName;
                var size = file.Length;
                // 可以存檔或讀取
            }

            return Content($"Uploaded {files.Count} files");
        }

        





    }

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        

        public HttpClient _httpClient = null;

        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            return Ok(new { Message = "Hello, Swagger!" });
        }

        [HttpPost("echo")]
        public IActionResult PostEcho([FromBody] object data)
        {
            return Ok(data);
        }

        [Authorize]
        [HttpGet("sss")]
        public IActionResult GetSecureData() => Ok("This is protected");

        [HttpPost("login")]
        public IActionResult Login(IConfiguration config)
        {
            string rerr = config.GetSection(key_app_setting.JWT)["SecretKey"]!;
            var token = GenerateToken("hahaha", config);

            return Ok(token);
        }

        

        [HttpPost("222")]
        public async Task<IActionResult> ttt(IHttpClientFactory httpClientFactory)
        {
            if (hahaha.circuitBreaker == null)
            {
                hahaha.circuitBreaker = Policy
                .Handle<HttpRequestException>()
                .CircuitBreaker(
                    exceptionsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromSeconds(10),
                    onBreak: (ex, ts) => {
                        int rrr = 0;
                        ha.Log_Program.LogWarning("rrr");
                    },
                    onReset: () => {
                        int rrr = 0;
                        ha.Log_Program.LogWarning("rrr");
                    },
                    onHalfOpen: () =>
                    {
                        int rrr = 0;
                        ha.Log_Program.LogWarning("rrr");
                    }
                );
            }
            if (_httpClient == null)
            {
                _httpClient = httpClientFactory.CreateClient();
                
            }
            try
            {
                var result = hahaha.circuitBreaker.Execute(() =>
                {
                    return _httpClient.GetStringAsync("https://example.com/api/data").GetAwaiter().GetResult();
                });
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"API 失敗: {ex.Message}");
            }
            catch (BrokenCircuitException)
            {
                return StatusCode(503, "服務暫時不可用（斷路器打開）");
            }
        }


        public static string GenerateToken(string username, IConfiguration config)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt")["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }


}
