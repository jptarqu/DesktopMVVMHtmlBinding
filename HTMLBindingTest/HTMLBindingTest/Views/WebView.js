



var observed_objs = [];

function InitBindings()
{
    ko.applyBindings(observed_objs[0]);
}


function NotifyNETViewModel(obj_id, prop_name, new_value)
{
    if (window.external)
    {
        window.external.OnNewValue(obj_id, prop_name, new_value);
    }
}

     
    var Order  = function (obj_id) {
        this.__obj_id = obj_id;
     
		        this.ID = ko.observable();
		        //this.__netvalue_ID = null;
                this.ID.subscribe(function(newValue) {
                    //if (this.__netvalue_ID != newValue) WE LEAVE THE CHNAGE CHECK TO .NET
                    //{
                        NotifyNETViewModel( obj_id, 'ID' , newValue);
                    //}
                });
			
	          }
        
        function Create_Order(new_id) { 
                observed_objs[new_id] = new Order(new_id); 
            }
 
        function Set_Order_ID(obj_id, new_value) { 
            //observed_objs[obj_id].__netvalue_ID = new_value;
            observed_objs[obj_id].ID(new_value); 
            }


     
    var Customer  = function (obj_id) {
        this.__obj_id = obj_id;
       
		        this.CustName = ko.observable();
		        //this.__netvalue_CustName = null;
                this.CustName.subscribe(function(newValue) {
                    //if (this.__netvalue_CustName != newValue) WE LEAVE THE CHNAGE CHECK TO .NET
                    //{
                        NotifyNETViewModel( obj_id, 'CustName' , newValue);
                    //}
                });
			
	   
		        this.Orders = ko.observableArray(); 
	         		
	     
		        this.CustNum = ko.observable();
		        //this.__netvalue_CustNum = null;
                this.CustNum.subscribe(function(newValue) {
                    //if (this.__netvalue_CustNum != newValue) WE LEAVE THE CHNAGE CHECK TO .NET
                    //{
                        NotifyNETViewModel( obj_id, 'CustNum' , newValue);
                    //}
                });
			
	          }
        
        function Create_Customer(new_id) { 
                observed_objs[new_id] = new Customer(new_id); 
            }
 
        function Set_Customer_CustName(obj_id, new_value) { 
            //observed_objs[obj_id].__netvalue_CustName = new_value;
            observed_objs[obj_id].CustName(new_value); 
            }
 
        function Set_Customer_CustNum(obj_id, new_value) { 
            //observed_objs[obj_id].__netvalue_CustNum = new_value;
            observed_objs[obj_id].CustNum(new_value); 
            }

 
        function Add_Customer_Orders(obj_id, inserted_obj_id) { observed_objs[obj_id].Orders.push(inserted_obj_id); }

    
