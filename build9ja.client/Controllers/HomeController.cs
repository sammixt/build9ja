using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using build9ja.client.Models;
using build9ja.core.Interfaces;
using AutoMapper;
using build9ja.core.Entities;
using build9ja.client.Dto;
using Microsoft.AspNetCore.Identity;
using build9ja.core.Entities.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace build9ja.client.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoryService _categoryService;
    private readonly IBannerService _bannerService;

    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger,ICategoryService categoryService, 
    IMapper mapper,IBannerService bannerService,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _logger = logger;
        _categoryService = categoryService;
        _mapper = mapper;
        _bannerService = bannerService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ActionResult> Index()
    {
        HomePageModel model = new HomePageModel();
         var categories = await _categoryService.GetCategoryByParentId(0,false);
            var categoriesDto = _mapper.Map<List<Category>, List<CategoryAndSubDto>>(categories);
            foreach(var categoryDto in categoriesDto){
               var categoriesByPId = await _categoryService.GetCategoryByParentId(categoryDto.Id,false);
                var cateListDto =  _mapper.Map<List<Category>, List<CategoryListDto>>(categoriesByPId);
                categoryDto.SubCategories = cateListDto;
            }
            var topCategories = await _categoryService.GetTopCategories();
            var topCategoriesDto = _mapper.Map<List<Category>, List<CategoryAndSubDto>>(categories);

            var banner = await _bannerService.GetBanners();
            var bannerDto = _mapper.Map<Banner,BannerDto>(banner);
            model.Banner = bannerDto;
            model.Category = categoriesDto.Take(11).ToList();
            model.TopCategoryA = topCategoriesDto.Skip(0).Take(1).FirstOrDefault();
            model.TopCategoryB = topCategoriesDto.Skip(1).Take(1).FirstOrDefault();
            model.TopCategoryC = topCategoriesDto.Skip(2).Take(1).FirstOrDefault();
            model.TopCategoryD = topCategoriesDto.Skip(3).Take(1).FirstOrDefault();
        return PartialView(model);
    }

    public async Task<ActionResult> Login(string returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> ProcessRegister([FromBody]RegisterDto registerDto )
    {
        if (CheckEmailExist(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new  {statusCode = 400, message =  "Email Address is in user"  });
            }

            if (!ModelState.IsValid)
            {
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                return new BadRequestObjectResult(new  { error = modelErrors });
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new {statusCode = 400, message = "Unable to create user"});
            if(string.IsNullOrEmpty(registerDto.ReturnUrl)){
                return Ok(new {statusCode = 200, message = $"Home/Index"});
            }
            // Controller/Action
            string[] splitReturnUrl = registerDto.ReturnUrl.Split("/");
            return Ok(new {statusCode = 200, message = $"{splitReturnUrl[0]}/{splitReturnUrl[1]}"});
                
    }

        [HttpPost]
        public async Task<ActionResult> Logon([FromBody]LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new {statusCode = 400, message = "User not found"});

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new {statusCode = 400, message = "User not found"});

            var claims = new List<Claim>();
                
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
                claims.Add(new Claim(ClaimTypes.GivenName, (user.FirstName + " " + user.LastName)));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.SerialNumber, user.Id));
                
                var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties()
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                    IsPersistent = true,
                    AllowRefresh = true
                };
                    //props.IsPersistent = model.RememberMe;
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                if(string.IsNullOrEmpty(loginDto.ReturnUrl)){
                    return Ok(new {statusCode = 200, message = $"Home/Index"});
                }
                // Controller/Action
                string[] splitReturnUrl = loginDto.ReturnUrl.Split("/");
                return Ok(new {statusCode = 200, message = $"{splitReturnUrl[0]}/{splitReturnUrl[1]}"});
        }

    [HttpGet]
    public async Task<ActionResult<Boolean>> CheckEmailExist([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

