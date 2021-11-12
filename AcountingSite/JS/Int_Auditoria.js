
var Int_Auditoria = {
    init: function () {
     
      
    },

      //VIEW THE  INT_SALDOS..
     Int_AuditoriaALL: function () {
 
           var data = {
            'username': 'HOLA'
        };     
        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Int_Auditoria/GetIntAuditoria",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",
            success: function (data) {
         
                $("#gridAuditoriaAll").kendoGrid({
                    dataSource: data,
                    pageable: true,
                       filterable: {
                            mode: "row"
                        },
                    selectable: true,
                    columns: [
                        { field: "EVENTO", title: "EVENTO",  width: 150 },
                        { field: "FECHA", title: "FECHA",  width: 150},
                        { field: "TABLA", title: "TABLA",  width: 150 },
                        { field: "USUARIO", title: "USUARIO",  width: 150 },
                        { field: "VALORNUEVO", title: "VALORNUEVO",  width: 150},
                        { field: "VALORVIEJO", title: "VALORVIEJO",  width: 150},
                       
                        ],editable: "incell",
                       
                 });
            },
            error: function (data) {
            }
        });  
    }

      
};

$(document).ready(function () {
    Int_Auditoria.init();
});
