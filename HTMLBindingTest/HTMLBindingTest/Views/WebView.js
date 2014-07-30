



var observed_objs = [];

function InitBindings()
{
    ko.applyBindings(observed_objs[0]);
}

function NotifyNETViewModel(obj_id, new_value)
{
    
}

     
    var Order  = function (obj_id) {
     
		        this.ID = ko.observable();
                this.ID.subscribe(function(newValue) {
                    NotifyNETViewModel( obj_id, newValue);
                });
			
	          }
        
        var Create_Order = function(new_id) { 
                observed_objs[new_id] = new Order(); 
            }
 
        var Set_Order_ID = function(obj_id, new_value) { observed_objs[obj_id].ID(new_value); }


     
    var Customer  = function (obj_id) {
       
		        this.CustName = ko.observable();
                this.CustName.subscribe(function(newValue) {
                    NotifyNETViewModel( obj_id, newValue);
                });
			
	   
		        this.Orders = ko.observableArray(); 
	         		
	     
		        this.CustNum = ko.observable();
                this.CustNum.subscribe(function(newValue) {
                    NotifyNETViewModel( obj_id, newValue);
                });
			
	          }
        
        var Create_Customer = function(new_id) { 
                observed_objs[new_id] = new Customer(); 
            }
 
        var Set_Customer_CustName = function(obj_id, new_value) { observed_objs[obj_id].CustName(new_value); }
 
        var Set_Customer_CustNum = function(obj_id, new_value) { observed_objs[obj_id].CustNum(new_value); }

 
        var Add_Customer_Orders = function(obj_id, inserted_obj_id) { observed_objs[obj_id].Orders.push(inserted_obj_id); }

    
