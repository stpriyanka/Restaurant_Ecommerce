# Cloud based Web Application Knowledge Base
### Sofware Development for Web (DA544B)
### OverView: 
The project idea of “Cloud-based E-commerce solution” has been developed on ASP.Net platform using Entity Framework MVC4. 

#### Application feature based on project scenario: 
  
1. Register & login facility including form authentication & authorization 
** Authentication **
Basic authentication is performed within the context of a "realm." The server includes the name of the realm in the WWW-Authenticate header. The user's credentials are valid within that realm. The exact scope of a realm is defined by the server.

For register user and login operation Basic authentication has been implemented. `AccountController` responsible for authenticating user. `LoginViewModel` a very simple and basic view model passes request credentials data from form.
Inside Account controller `Register()` for `[Get]` and `[Post]` requests has been implemented.


**Authorizarion**
Role based authorization: Multi role based authorization has been implemented to restrict control over user access.

All the correspondong views are rendered by @razor view engine. For login and register concerned views are located in `View > Account` 
folder

`Manage controller` is mainly responsible for password reset, and its principles. The back end logic is prepared but did not implemented in View.

I have added 3 different Multi-role Security: 
   - Super Admin
   - Admin
   - General
Role based authorization checks are declarative on codes. The logic has been implemented in `SuperAdminController`. I have specified roles which the current user should be a member of to access the requested resource.  

 ```
 public class SuperadminController : AccountController
	{
		// GET: Superadmin
		[Authorize(Roles = "SuperAdmin")]
		public ActionResult Index() 
		{
			var db = new ApplicationDbContext();
			//get users 
			List<ApplicationUser> users = db.Users.ToList();
			var roles = db.Roles.ToList();
      .........
  ```

By definiing `role` in `[Authorize]` attribute I have set special access for `SuperAdmin`. I have mentioned in `_Layout.cshtml` class which vew option should be rendenred for different access users.

Here is the code snipet.
   
   ```
    			@if (Request.IsAuthenticated && User.IsInRole("SuperAdmin"))
					{
						<li>@Html.ActionLink("Users", "Index", "Superadmin")</li>
					}

					@if (Request.IsAuthenticated && User.IsInRole("Admin"))
					{
						<li>@Html.ActionLink("Admin", "Index", "FoodCategories")</li>
					}
    ```
In `SuperAdminController` function called `updateRole()` is the logic that only superadmin has access to do, swap role access between `SuperAdmin`, `Admin` or `General`; view and monitor all existed users roles.


3. Cross-domain connection facility 
   - **PayPal API**: I have used public API to connect with PayPal. The root logic for payment has been implemented in `Root > Controller > Payment > PaymentController`. From server a [HttpPost] request `PaymentConfirmation` function has been called to integrate with Paypal. I also added some extra fields in the URL of my request as a query string. 
   
      ```
   	[HttpPost]
		public void PaymentConfirmation(string name, string amount, string personNumber, string phoneNumber)
		{
			var path = "http://dhakafood.azurewebsites.net/";
			string redirecturl = "";
			redirecturl += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + "{MONEY RECEIVER EMAIL}";
			redirecturl += "&first_name=" + name;
			redirecturl += "&amount=" + amount;
      redirecturl += "&currency_code=" + "SEK";
			redirecturl += "&item_number=1" + personNumber; //to identify payment from list
			redirecturl += "&return=" + path;
      ..................
      ```
The entire test has been completed in their sandbox environment. [PayPal Sandbox][https://www.sandbox.paypal.com/us/home]


   
   
   
   - Google API

5. Real time communication or two way message exchange using SignalR

Compatible with all major browsers
   - Chrome
   - Firefox
   - IE 8+
2. Integration with social networking sites face book
6. 
    

 
