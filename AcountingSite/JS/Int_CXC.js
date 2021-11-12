
var Int_CXC = {
    init: function () {     
       Int_CXC.createdatePicker();
    },

    createdatePicker: function () {

           $("#datepickerstarCXC").kendoDatePicker({
                format: "MM/dd/yyyy"
              })
              .closest(".k-widget")
              .attr("id", "datepicker_wrapper");  

           $("#datepickerendCXC").kendoDatePicker({
                format: "MM/dd/yyyy"
              })
              .closest(".k-widget")
              .attr("id", "datepicker_wrapper") ;                
            },

      //VIEW THE  INT_SALDOS..
     Int_CXCALL: function () {
    
   var datepickerstar = $("#datepickerstarCXC").data("kendoDatePicker");
   var datepickerend = $("#datepickerendCXC").data("kendoDatePicker");


 $("#btnAplicarCXC").click(function () {   

        datestar = $("#datepickerstarCXC").val();
        dateend = $("#datepickerendCXC").val();


           var data = {
            'datestar': datestar,
            'dateend': dateend 
        };          
        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Int_CXC/GetInt_CXCALL",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",
            success: function (data) {
          
                $("#gridCXCAll").kendoGrid({
                    dataSource: data,
                    pageable: true,
                       filterable: {
                            mode: "row"
                        },
                    selectable: true,
                    columns: [
                        { field: "NUM_FACTURA", title: "NUM_FACTURA",  width: 150},
                        { field: "NOMBRE_PROVEEDOR", title: "NOMBRE_PROVEEDOR",  width: 150},
                        { field: "CENTRO_COSTO", title: "CENTRO_COSTO",  width: 150 },
                        { field: "COD_COMPANIA", title: "COD_COMPANIA",  width: 150},
                        { field: "COD_PAIS", title: "COD_PAIS",  width: 150 },
                        { field: "COD_PROVEEDOR", title: "COD_PROVEEDOR",  width: 150 },
                        { field: "FECHA", title: "FECHA",  width: 150},
                        { field: "IVA", title: "IVA",  width: 150},
                        { field: "MONEDA", title: "MONEDA",  width: 150},
                        { field: "MONTO_SIN_IVA", title: "MONTO_SIN_IVA",  width: 150},
                        { field: "NIT_PROVEEDOR", title: "NIT_PROVEEDOR",  width: 150},
                        { field: "NOMBRE_COMPANIA", title: "NOMBRE_COMPANIA",  width: 150},
                        { field: "NOMBRE_PAIS", title: "NOMBRE_PAIS",  width: 150},                       
                       ],editable: "incell",
                       
                 });
            },
            error: function (data) {
            }
        });  
     });
    }

      
};

$(document).ready(function () {
    Int_CXC.init();
});
