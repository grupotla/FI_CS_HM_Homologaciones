
var Int_Saldos = {
    init: function () {
     Int_Saldos.createdatePicker();
      
    },
    createdatePicker: function () {

           $("#datepickerstar").kendoDatePicker({
                format: "MM/dd/yyyy"
              })
              .closest(".k-widget")
              .attr("id", "datepicker_wrapper");  

           $("#datepickerend").kendoDatePicker({
                format: "MM/dd/yyyy"
              })
              .closest(".k-widget")
              .attr("id", "datepicker_wrapper") ;                
            },

      //VIEW THE  INT_SALDOS..
     Int_saldosALL: function () {

  var datepickerstar = $("#datepickerstar").data("kendoDatePicker");
   var datepickerend = $("#datepickerstar").data("kendoDatePicker");


 $("#btnAplicar").click(function () {   

        datestar = $("#datepickerstar").val();
        dateend = $("#datepickerend").val();


           var data = {
            'datestar': datestar,
            'dateend': dateend 
        };     
        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Int_Saldos/GetIntSaldosDiarios",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",
            success: function (data) {
          debugger;
                $("#gridSaldosDiariosAll").kendoGrid({
                    dataSource: data,
                    pageable: true,
                       filterable: {
                            mode: "row"
                        },
                    selectable: true,
                    columns: [
                        { field: "ASIENTO", title: "ASIENTO",  width: 150 },
                        { field: "COD_COMPANIA", title: "Codigo COMPANIA",  width: 150},
                        { field: "COD_PAIS", title: "Codigo Pais",  width: 150 },
                        { field: "CREDITOS_LOCAL", title: "CREDITOS LOCAL",  width: 150 },
                        { field: "CUENTA_CONTABLE", title: "CUENTA CONTABLE",  width: 150},
                        { field: "DEBITO_DOLAR", title: "DEBITO DOLAR",  width: 150},
                        { field: "DEBITO_LOCAL", title: "DEBITO LOCAL",  width: 150},
                        { field: "DESCRIPCION_NIT", title: "DESCRIPCION NIT",  width: 150},
                        { field: "DESCRIPCIÓN", title: "DESCRIPCIÓN",  width: 150},
                        { field: "FECHA", title: "FECHA",  width: 150},
                        { field: "FUENTE", title: "FUENTE",  width: 150},     
                        { field: "NIT", title: "NIT",  width: 150},
                        { field: "NOMBRE_COMPANIA", title: "NOMBRE COMPANIA",  width: 150},
                        { field: "NOMBRE_PAIS", title: "NOMBRE PAIS",  width: 150},
                        { field: "ORIGEN", title: "ORIGEN",  width: 150},
                        { field: "PAQUETE", title: "PAQUETE",  width: 150},
                        { field: "REFERENCIA", title: "REFERENCIA",  width: 150},
                        { field: "TIPO_ASIENTO", title: "TIPO ASIENTO",  width: 150},
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
    Int_Saldos.init();
});
