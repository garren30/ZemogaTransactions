# ZemogaTransactions
Project to upload  code and bd to Zemoga Test


#Data
	You can find three folders, each one has the data that I wil explain:
		#	Code: you will find the complete source code for the mvc project(TransactionsWebApp) and webapi project (TransactionsApi), also you can see a sql file (	BackUpBD_Transactions.sql) with the structure for the db and the basic information that toy have to write on the db (Roles, TransactionTypes)

		#	Publishable: You will find the Publishable for the mvc project (Web) and webApi (Api), also you can see a bak file (BackUpBD_Transactions.bak) to restore this on your database, this file allready has the basic information

		#	Driagrams: You cab find the diagrams, with their names.



#deploy
	1) Download the repository from https://github.com/garren30/ZemogaTransactions.git
	2) Create a new db as TransactionsBD on sqlserver 2012 or higher with windows authentication
	3) Restore the BackUpBD_Transactions.bak file in the db that you create on step 2, this file is in /ZemogaTransactions/Publishable/BackUpBD_Transactions.bak
	4) Check that the tables roles and transactiontypes have information, otherwise delete the db an do steps 2 and 3
	5) Move the Publishable for web and api to the folder that you are going to publish it(could be the same folder that are now) the folders you have to move are "/ZemogaTransactions/Publishable/Web" and  "/ZemogaTransactions/Publishable/Api"
	6) Open the IIS Administrator (You should have already configurate your IIS)
	7) right click on sites and selet add web site
	8) Write the name to your website, select the folder where you put the Publishable, click on connect as and put the user's info that have permissons on the server, finally select the ip and port on which it will run.
	9) After thar you go to the publishable folder and open de web.config file, in the tag <connectionStrings> be sure that you change the source for your server name or database instance and if you create the database with other name change it in catalog.

	9) you have to repeat steps 7, 8 and 9 to configurate the api project (in the step 8 select the folder that you did not select before)
	10) now you can go to the browser and put the ip and port to see that is now working

	11) In the db you can find 4 user to see how it works:
		user: Administrator, pass: Administrator
		user: Superintendent, pass: Superintendent
		user: Manager, pass: Manager
		user: Assistant, pass: Assistant

	





# How to work
	#	Api: if you want to use this apis you have to send in the header de user id as user, you can see hoy to do with ajax:
		var objeto = $.ajax({
                        type: "GET",
                        url: your_server + "api/Transactions/GetTransactionList",
                        contentType: 'application/json',
                        cache: false,
                        beforeSend: function (xhr) {
							xhr.setRequestHeader("User", 1);
                            //xhr.setRequestHeader("User", userid);
                        },
                        success: function () 
			            {
			                
			            },
			            error: function () {
			                
			            }
			        });


# Contact Information
	Name: CARLOS HERNAN DIAZ BROCHERO
	Phone: 3188278807
	Email: CARLOSH.86.DIAZ@GMAIL.COM
