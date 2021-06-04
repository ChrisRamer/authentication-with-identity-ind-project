# *Pierre's Sweet and Savory Treats*

#### *Pierre's Sweet and Savory Treats, 06/04/2021*

#### By **Chris Ramer**

## Description

An application for my good pal, Pierre. An app to market his sweet and savory treats with user authentication and a many-to-many relationship for the database.

## Setup/Installation Requirements

Clone this repo.

Navigate to the `SavoryTreats` directory in the Terminal and run the command `dotnet ef database update` to create the database.

Then run the command `dotnet run` and open `localhost:5000` in your browser.

## Specs

* **Spec:** Adding a new flavor will add that flavor to a list of all flavors
* **Input:** Flavor Cinnamon
* **Output:** Redirects to page showing the details for Cinnamon flavor
* **Spec:** Adding a new treat will add that treat to a list of all treats
* **Input:** Treat Cinnamon Roll
* **Output:** Redirects to page showing the details for Cinnamon Rolls
* **Spec:** Clicking "Add treat" link on the flavor page allows you to add treats to that flavor
* **Input:** Add treat Chocolate Chip Cookie on Chocolate flavor page
* **Output:** Redirects to Chocolate flavor page with the list of treats updated to show Chocolate Chip Cookie in the list
* **Spec:** Clicking "Add flavor" link in the treat page allows you to add flavors to that treat
* **Input:** Add Cinnamon flavor to Cinnamon Roll
* **Output:** Redirects to Cinnamon Roll details page with the list of flavors updated to show Cinnamon in the list

## Technologies Used

* C#
* ASP.NET
* .NET Core

### License

Copyright (c) 2021 **Chris Ramer**
This software is licensed under the MIT license.