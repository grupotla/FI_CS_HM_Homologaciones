
var Int_Ingresos_CXC = {
    init: function () {
    Int_Ingresos_CXC.createdatePicker();
      
    },
    createdatePicker: function () {

           $("#datepickerstaringresos").kendoDatePicker({
                format: "MM/dd/yyyy"
              })
              .closest(".k-widget")
              .attr("id", "datepicker_wrapper");  

           $("#datepickerendingresos").kendoDatePicker({
                format: "MM/dd/yyyy"
              })
              .closest(".k-widget")
              .attr("id", "datepicker_wrapper") ;                
            },

      //VIEW THE  INT_SALDOS..
     Int_Ingresos_cxcALL: function () {

  var datepickerstar = $("#datepickerstaringresos").data("kendoDatePicker");
   var datepickerend = $("#datepickerendingresos").data("kendoDatePicker");


 $("#btnAplicaringresos").click(function () {   

        datestar = $("#datepickerstaringresos").val();
        dateend = $("#datepickerendingresos").val();


           var data = {
            'datestar': datestar,
            'dateend': dateend 
        };     
        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Int_Ingresos_CXC/GetInt_Ingresos_CXC",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",
            success: function (data) {
          debugger;
                $("#gridingresosAll").kendoGrid({
                    dataSource: data,
                    pageable: true,
                       filterable: {
                            mode: "row"
                        },
                    selectable: true,
                    columns: [
                        { field: "ASIENTO", title: "ASIENTO",  width: 150 },    
                        { field: "CUENTA_CONTABLE_ENC", title: "CUENTA CONTABLE ENC",  width: 200},
                        { field: "CUENTA_CONTABLE_ENC", title: "CUENTA CONTABLE ENC",  width: 200},
                        { field: "CENTRO_COSTO_ENC", title: "CENTRO DE COSTO ENC",  width: 200 },
                        { field: "CENTRO_COSTO_DET", title: "CENTRO DE COSTO DET",  width: 200 },
                        { field: "COD_COMPANIA", title: "CODIGO COMPANIA",  width: 150},
                        { field: "COD_PAIS", title: "CODIGO PAIS",  width: 150 },
                        { field: "CREDITOS_DOLAR", title: "CREDITOS DOLAR",  width: 150 },
                        { field: "CREDITOS_LOCAL", title: "CREDITOS LOCAL",  width: 150 },
                        { field: "DEBITO_DOLAR", title: "DEBITO DOLAR",  width: 150},
                        { field: "DEBITO_LOCAL", title: "DEBITO LOCAL",  width: 150},
                        { field: "DESCRIPCION_NIT", title: "DESCRIPCION NIT",  width: 150},
                        { field: "DESCRIPCIÓN", title: "DESCRIPCIÓN",  width: 150},
                        { field: "FECHA", title: "FECHA",  width: 150},
                        { field: "FUENTE", title: "FUENTE",  width: 150},
                        { field: "IVA", title: "IVA",  width: 150},                             
                        { field: "MONEDA", title: "MONEDA",  width: 150},
                        { field: "MONTO_SIN_IVA", title: "MONTO_SIN_IVA",  width: 150},
                        { field: "NIT", title: "NIT",  width: 150},
                        { field: "TIPO_CAMBIO", title: "TIPO CAMBIO",  width: 150},                        
                        { field: "NOMBRE_COMPANIA", title: "NOMBRE COMPANIA",  width: 150},
                        { field: "NOMBRE_PAIS", title: "NOMBRE PAIS",  width: 150},
                        { field: "ORIGEN", title: "ORIGEN",  width: 150},
                        { field: "PAQUETE", title: "PAQUETE",  width: 150},
                        { field: "REFERENCIA", title: "REFERENCIA",  width: 150},
                        { field: "TIPO_ASIENTO", title: "TIPO ASIENTO",  width: 150},
                        { field: "ESTADO", title: "ESTADO",  width: 150},
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
    Int_Ingresos_CXC.init();
});
