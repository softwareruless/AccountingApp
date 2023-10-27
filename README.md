# Accounting App

You can track your stocks in this app. Create a product, set a stock amount and create your customer. Sell or buy products and stock amount will be changed. Also the amount of money in your bank account will change  <br>
After transaction make a invoice. Track your income and expenses from the home screen.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

--Check the connection string on Accounting.Data/Model/DataContext.cs file. <br>

Comment [AdminSessionControl] attribute from /Account/Add functions in Areas/Admin/Controllers/AccountController . Go to localhost:xxxx/Admin/Account/Add page and add new User. Uncomment [AdminSessionControl] attribute after create user.

First the request comes to the controller (ProductController), then the DataAccess runs (ProductDataAccess) and changes occur in the database.

.Net version 6 <br>
Microsoft.EntityFrameworkCore 5.0.17 <br>

## Authors

* **Yusuf Bozkurt** - *Initial work* - [softwareruless](https://github.com/softwareruless)

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
