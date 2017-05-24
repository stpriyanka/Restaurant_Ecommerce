# Cloud based Web Application Project Documentation
## Course: Sofware Development for Web (DA544B)
### Computer Software Development
#### Examiner: Dawit Mengistu
**Written by: Safwath Tasnia Priyanka (910112-2449)**

### Overview:
___

The project idea of “Cloud-based e-commerce solution” has been developed on ASP.Net platform as a part of this course 'Software Development for Web'. The aim of this document is to demonstrate technical details and explain logics behind code in a deeper level to understand the knowledge base of this application.

### Technical Details :
___

**Code first approach using Entity Framework**

The Code First Approach provides an alternative to the `Database First` and `Model First` approaches to the Entity Data Model and creates a database based on model classes that is located in `root > Models>` in solution.

![alt text](https://s15.postimg.org/yt3qd0ji3/codefirst.jpg)

I have chosen `Code-First` as 
- I don't like any kind of designers and defining mapping in database manually.
- I will always have full control over the code (no auto-generated code which is hard to modify).
- I don't need to spend time with DB. DB is just a storage with no logic. EF will handle CRUD operations. more on CRUD operations related to this application has been described in further section.

**MVC 4**

The application has been developed in MVC 4. I have chosen this as using MVC in application enables option for scaling and also it is highly testable. MVC framework includes the following components:

![alt text](https://s28.postimg.org/5k4jdkzp9/d_Quk_P.png)

- **Model:**

model objects retrieve and store model state in a database. For example, a Food object might retrieve information from a database, operate on it, and then write updated information back to a Foods table in a SQL Server database.

- **View:**

Views are the components that display the application's user interface (UI).

- **Controller:**

Controllers are the components that handle user interaction, work with the model, and ultimately select a view to render that displays UI. In an MVC application, the view only displays information; the controller handles and responds to user input and interaction.



#### 1. Register & login facility including form authentication & authorization
___

**Authentication**

Basic authentication is performed within the context of a "realm." The server includes the name of the realm in the WWW-Authenticate header. The user's credentials are valid within that realm. The exact scope of a realm is defined by the server.

For register user and login operation Basic authentication has been implemented. `AccountController` responsible for authenticating user. `LoginViewModel` a very simple and basic view model passes request credentials data from form.
Inside Account controller `Register()` for `[Get]` and `[Post]` requests has been implemented.


**Authorization**

**Role based authorization:** 

Multi role based authorization has been implemented to restrict control over user access.

All the corresponding views are rendered by @razor view engine. For login and register users concerned views are located in `Views > Account` folder

`Manage controller` is mainly responsible for password reset, and its principles. The back end logic is prepared but did not rendered in View.

I have implemented 3 different role based user access  : 
   - Super Admin
   - Admin
   - General

'Role' based authorization-checks are declarative on codes. The logic has been implemented in `SuperAdminController`. I have specified roles which the current user should be a member of to access the requested resource.  

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

By defining `role` in `[Authorize]` attribute I have set special access for `SuperAdmin`. I have mentioned in `_Layout.cshtml` class which view should be rendered for users with different access.

Here is the code snippet.

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

In `SuperAdminController` a function has been implemented called `UpdateRole()` which is the logic that, only super admin has access to do, such as swap role access between `SuperAdmin`, `Admin` or `General`, view all registered users and keep monitoring about users roles.


#### 2. Cross-Domain Integration (CORS)
___

#### PayPal API: 

I have used PayPal public API to connect with this application. The root logic for payment has been implemented in `Root > Controller > Payment > PaymentController.cs` class. From server a [HttpPost] request `PaymentConfirmation()` function has been called to integrate with Paypal API. I also added some extra fields in the URL of my request as a query string such as shipping cost, return URL.

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

All the payment transactions for this application has been completed in PayPal's sandbox environment. [PayPal Sandbox](https://www.sandbox.paypal.com/us/home). Other than that, credit card processing, payment and all others respsiblity were handle by the API request.   

#### Google API

A CORS request to a Google API is similar to a REST request. Google provides free API to integrate Google Maps for non-commercial purpose.

Enable API Key

Google APIs use the OAuth 2.0 protocol for authentication and authorization. So, every application requests an access token from the Google Authorization Server, extracts a token from the response, and sends the token to the Google API that I added in request URL.

To use the Google Maps Geocoding API, I had to register this application by creating an app on `Google API Console` to get a Google API key which I later on added in my code as a query string before sending request.

in `Contact.cshtml` page this is how I integrated Google Map API key adding with public URL.

  ```

<script src="https://maps.googleapis.com/maps/api/js?key={Added-My-API-key:dfmkvhjggfgb}"></script>
  
  ```

Since I have a mentioned a specific address in Google Map, So I added direct latitude and longitude for that address inside code.

Code snippet is given below:

   ```
   
<script>
	var myCenter = new google.maps.LatLng(59.402986, 17.944129);
	var marker;
         ....
  
   ```
  
#### 3. Real Time Communication
___

In this application online live chat application has been implemented using `SignalR`, a real time web framework. I have used SignalR version 2. Real-time web feature is the ability to have server code push content to connected clients immediately. The SignalR API contains two models for communicating between clients and servers: Persistent Connections and Hubs. A Hub is a more high-level pipeline built upon the Connection API that allows your client and server to call methods on each other directly.

This is how I set configuration to enable public chat room where messages broadcast to all connected users.

![alt text](https://s7.postimg.org/l129ilzpn/3250.Brij_pic_3.png)

In my solution root, a hub class has been created named `chatHub.cs` that works as a middle ware for sending and receiving data back and forth to clients and server. 

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

  ```

#### 4.LINQ Query
___

I have used LINQ to SQL everywhere in the application because as I have only used SQL server, LINQ to SQL is considered faster. 
In every places where it needed I use LINQ query to get entity from database.

- Scenario 1 : In application home page user are offered to click to see lunch specials. Using LINQ :

[C#] Example 1: Query `FoodITem` by `CategoryName`

```
	public ActionResult LunchFilter()
	{
		var x1 = db.Foods.Where(x => x.CategoryName == "Lunch").ToList();
		return View("Index",x1);
	}

```
View is rendered in `root > Views > Home > Index.cshtml` class.

- Scenario 2 : Only authenticated users should be able to rate foods and it will update related view instantly. Using LINQ:

[C#] Example 1: Rate Food and update rating status in database instantly. the code is implemented in `OrderController`

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
___

In this application I have performed CRUD (create, read, update, delete) operation using entity framework. I have used MVC helpers to generate views and then modified as per my requirements. 

For `Food` and `FoodCategory` entities I have performed CRUD operations. The backend logic for both of these entities reside in `FoodCategoryController.cs` class.

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

To **update** specific item in `Food` entity 

```
	db.Entry(foodName).State = EntityState.Modified;
	db.SaveChanges();
```

To **delete** specific item in `Food` entity 

```
	Food foodName = db.Foods.Find(id);
	db.Foods.Remove(foodName);
	db.SaveChanges();
```

To **read** or render entities `Index()` function from `FoodController` has been used.

```

public ActionResult Index(string currentFilter, int? page)
{
	var q = db.FoodCategories.OrderBy(r => r.CategoryName).ToList();
	int pageSize = 3;
	int pageNumber = (page ?? 1);
	return View(q.ToPagedList(pageNumber, pageSize));
}

```

#### 6. Integration with social networking sites face book
___

`_Layout.cshtml` class I have added scipts that is required to request Facebook public API to share application link in active login user's profile. In `_footer.cshtml` class has the html and public API that makes request to Facebook to share desire link.

 ```
 
<form id="form1" runat="server">
	<a href="https://www.facebook.com/sharer/sharer.php?u="><i id="social-fb" class="fa fa-facebook-square fa-3x social"></i></a>
	<p>&copy; @DateTime.Now.Year - Dhaka Food Restaurant & Bar </p>
</form>

 ```

#### 7.Host application and database on Azure cloud service
___

This is URL hosted in azure. [Dhaka Food Restaurant.](http://dhakafood.azurewebsites.net/). I have also hosted a SQL Server database in Azure and configure connectionString in `Web.Config` and publish this application from local. 

Database connectionString before deploying application to Azure should be this.

```
<!--<add name="RestaurantContext" connectionString="Server=tcp:dhakafood.database.windows.net,1433;Initial Catalog=RestaurantContext;Persist Security Info=False; UserID=dhakafood; Password=Dbuser12@;MultipleActiveResultSets=False;Encrypt=True; TrustServerCertificate=False;Connection Timeout=30;" providerName="System.Data.SqlClient" />--> 

```

If the application is running from `locahost` make sure the `connectionString` for database is back to `.\SQLExpress` instead the one mentioned above.
This is how from localhost the connectionString should look alike.

```
<connectionStrings>
<add name="RestaurantContext" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=RestaurantContext;Integrated Security=SSPI" providerName="System.Data.SqlClient" />
</connectionStrings>
```
	
___
