# Cloud based Web Application Knowledge Base
### Sofware Development for Web (DA544B)
### OverView:
The project idea of “Cloud-based E-commerce solution” has been developed on ASP.Net platform using Entity Framework MVC4.

### Application feature based on project scenario:

#### 1. Register & login facility including form authentication & authorization **

** Authentication **

Basic authentication is performed within the context of a "realm." The server includes the name of the realm in the WWW-Authenticate header. The user's credentials are valid within that realm. The exact scope of a realm is defined by the server.

For register user and login operation Basic authentication has been implemented. `AccountController` responsible for authenticating user. `LoginViewModel` a very simple and basic view model passes request credentials data from form.
Inside Account controller `Register()` for `[Get]` and `[Post]` requests has been implemented.


**Authorizarion**

Role based authorization: Multi role based authorization has been implemented to restrict control over user access.

All the corresponding views are rendered by @razor view engine. For login and register users concerned views are located in `View > Account` folder

`Manage controller` is mainly responsible for password reset, and its principles. The back end logic is prepared but did not rendered in View.

I have added 3 different Multi-role based security:
   - Super Admin
   - Admin
   - General


'Role' based authorization checks are declarative on codes. The logic has been implemented in `SuperAdminController`. I have specified roles which the current user should be a member of to access the requested resource.  

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

By defining `role` in `[Authorize]` attribute I have set special access for `SuperAdmin`. I have mentioned in `_Layout.cshtml` class which view should be rendered for different access users.

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

In `SuperAdminController` controller a function called `UpdateRole()` is the logic that only super admin has access to do, such as swap role access between `SuperAdmin`, `Admin` or `General`, view all registered users and keep monitoring about users roles.


#### 2. Cross-domain Integration (CORS)

** PayPal API: **

 I have used public API to connect with PayPal. The root logic for payment has been implemented in `Root > Controller > Payment > PaymentController`. From server a [HttpPost] request `PaymentConfirmation` function has been called to integrate with Paypal. I also added some extra fields in the URL of my request as a query string.

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
Credit card processing, payment and all others are handle by the request.   

**Google API**

A CORS request to a Google API is similar to a REST request. Google provides the free API to integrate Google Maps for non-commercial purpose.

** Enable API Key **

Google APIs use the OAuth 2.0 protocol for authentication and authorization. So, every application requests an access token from the Google Authorization Server, extracts a token from the response, and sends the token to the Google API that developer add in their request URL.

To use the Google Maps Geocoding API, I had to register this application by creating a app on Google API Console and to get a Google API key which later on I added in my code as a query string.

in `Contact.cshtml` page this is how I integrated Google Map API key adding with public URL.

```
<script src="https://maps.googleapis.com/maps/api/js?key={Added my API key}"></script>
```

Since I have a mentioned specified address in Google Map , So I added direct latitude and longitude in my code.

Code snnipet is given below:

```
<script>
	var myCenter = new google.maps.LatLng(59.402986, 17.944129);
	var marker;
  .............
```
#### 3. Real time communication

Online live chat application has been implemented using SignalR, a real time web framework. I have used SignalR version 2. Real-time web feature is the ability to have server code push content to connected clients immediately. The SignalR API contains two models for communicating between clients and servers: Persistent Connections and Hubs. A Hub is a more high-level pipeline built upon the Connection API that allows your client and server to call methods on each other directly.

This is how I set configuration to enable public chat room where messages broadcast to all connected users.

![alt text](https://s7.postimg.org/l129ilzpn/3250.Brij_pic_3.png)

In my solution root a class has been created named `chatHub.cs` that works as a middle ware for sending and receiving data and back and forth to clients and server. 

The client side logic has been implemented in `chatScript.js` (`root > Scripts > chatScript.js`) class. The logic is really simple and pretty self explanatory yet I have added some comments on it.

```
    // Declare a proxy to reference the hub.
    var chat = $.connection.chatHub;

    console.log('Now connected, connection ID=' + $.connection.hub.id);

    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (name, message) {

    // Html encode display name and message.
    document.getElementById('start1').style.visibility = 'hidden';
    var encodedName = name;
    var encodedMsg = $('<div />').text(message).html();

    // Set current time
    var time = new Date();
    var currentTime = time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds();
    console.log(time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds());

    // Add the message to the page.
    $('#discussion').append('<li><strong>' + encodedName
      + '</strong>:&nbsp;&nbsp;' + encodedMsg + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;time:' + '&nbsp;&nbsp;' + currentTime + '</li>');

    //$('#time').append(currentTime);

    return;
    }

    // Get the user name and store it to prepend to messages.
    // Set initial focus to message input box.
    $('#message').focus();

    // Start the connection.
    $.connection.hub.start().done(function () {
    $('#sendmessage').click(function () {
    var name1 = document.querySelector('#start1').value;

    //if user dont want to set their name by default user will name after 'Guest'
    if (!name1) {
      name1 = "Guest";
    }

    // Call the Send method on the hub.
    chat.server.send(name1, $('#message').val());

    // Clear text box and reset focus for next comment.
    $('#message').val('').focus();
  });
});
........
```

#### 4.LINQ Query

I used LINQ to SQL everywhere in the application because I have only used SQL server and if I only use only SQL server then LINQ to SQL is considered faster. 

In every places where it needed I use LINQ query to get Entity from database.

- Scenario 1 : in home page user are offered to click to see lunch special for this restautant application. Using LINQ

[C#] Example 1: Query FoodITem by CategoryName

```
	public ActionResult LunchFilter()
	{
	var x1 = db.Foods.Where(x => x.CategoryName == "Lunch").ToList();
	return View("Index",x1);
	}

```
View is rendered in `root > View > Home > Index.cshtml` class.

- Scenario 2 : Only authenticated users should be able to rate foods and it will update view instantly. Using LINQ

[C#] Example 1: Rate FoodITem and update rating in database. the code is implemented in `OrderController`

```
	[HttpPost]
	public ActionResult RateFood(int? rate, int foodId)
		{
			if (rate != null)
			{
			var food = db.Foods.Find(foodId);

			food.RatingCount++;

			food.FoodRating += rate.Value;

			db.Entry(food).State = EntityState.Modified;
			db.SaveChanges();
			}
		return RedirectToAction("Index");
		}
```

#### 5.CRUD functionality with Entity Framework
In this application I have perfomred CRUD (create, read, update, delete) operation using entity framework. I have used MVC helpers to generate vies and then I modified as per my requirements. 

For `Food` and `FoodCategory` entities I have performed CRUD operationss. The back end logic for both resides in `FoodCategoryController.cs` class.

Example below shows **create** operation for `FoodCategory` entity.

```
// POST: FoodCategories/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create([Bind(Include = "ID,CategoryName,CategoryDescription")] FoodCategory foodCategories)
{
	if (ModelState.IsValid)
	{
		db.FoodCategories.Add(foodCategories);
		db.SaveChanges();
		return RedirectToAction("Index");
	}
	return View(foodCategories);
}
```

To **update** specific `Food` entity 

```
	db.Entry(foodName).State = EntityState.Modified;
	db.SaveChanges();
```

To **delete** specific `Food` entity 

```
	Food foodName = db.Foods.Find(id);
	db.Foods.Remove(foodName);
	db.SaveChanges();
```

To **read** or render entities `Index function` from `FoodController` is used.

```
	public ActionResult Index(string currentFilter, int? page)
	{
		var q = db.FoodCategories.OrderBy(r => r.CategoryName).ToList();
		int pageSize = 3;
		int pageNumber = (page ?? 1);
		return View(q.ToPagedList(pageNumber, pageSize));
	}
```

#### Compatible with all major browsers
   - Chrome
   - Firefox
   - IE 8+

#### 4. Integration with social networking sites face book
