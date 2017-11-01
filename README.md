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



