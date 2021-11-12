
var Acount_Cuentas = {
    init: function () {
     
      
    },


      //VIEW THE  HOMOLOCACION DE CUENTAS...
     HomCuentasGrid: function () {
 
           var data = {
            'username': 'HOLA'
        };     
        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Acount_Cuentas_API/GetHomCuentasGrid",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",
            success: function (data) {
         
                $("#gridHomCuentasContableAll").kendoGrid({
                    dataSource: data,
                    pageable: true,
                       filterable: {
                            mode: "row"
                        },
                    selectable: true,
                    columns: [
                        { field: "CUENTA_CONTABLE_ENC", title: "Cuenta Contable ENC", width: 195  },
                        { field: "CUENTA_CONTABLE_DET", title: "Cuenta Contable DET", width: 195  },
                        { field: "CENTRO_COSTO_ENC", title: "Centro Costo ENC", width: 195 },
                        { field: "CENTRO_COSTO_DET", title: "Centro Costo DET", width: 195 },
                        { field: "DIVISION", title: "Divicion",  width: 150 },
                        { field: "COD_COMPANIA", title: "Codigo Compañia", width: 140 },
                        { field: "COD_PAIS", title: "Codigo Pais", width: 120 },
                        { command: { text: "", name: "Create", iconClass: "btn fas fa-save", click: Acount_Cuentas.Create_HomCuentas }, title: "" , width: 60 },
                        { command: { text: "", name: "des", iconClass: "btn far fa-trash-alt", click: Acount_Cuentas.Delete_HomCuentas }, title: "", width: 60 },
                        ],editable: "incell",
                       toolbar:[{name:"create", text: "Crear Nueva Cuenta"}]
                 });
            },
            error: function (data) {
            }
        });  
    },

      //DELETE THE HOMOLOCACION DE TIPO DE DOCUMENTO...
     Delete_HomCuentas: function (e) {

        debugger;
    swal({
        
        icon: "warning",
        text: "¿Esta seguro que desea eliminarlo?",
        buttons: {
            cancel: "Cancelar",
            confirm: { text: "Aceptar", confirmButtonColor: '#044677' }
        },
        
    })
    .then((willDelete) => {
        if (willDelete) {
             debugger;
              var dataItem = this.dataItem($(e.target).closest("tr"));
              var grid = $("#gridHomCuentasContableAll").data("kendoGrid");
              var selectRow = $(e.currentTarget).closest("tr")
              var itemRowSelect = grid.dataItem(grid.select(selectRow))
              var grid2 = $("#gridHomCuentasContableAll").data("kendoGrid");
              var selectedItem = grid2.dataItem(grid2.select());

              var CUENTA_CONTABLE_ENC = (selectedItem.CUENTA_CONTABLE_ENC);
              var CENTRO_COSTO_ENC = (selectedItem.CENTRO_COSTO_ENC);
              var CUENTA_CONTABLE_DET = (selectedItem.CUENTA_CONTABLE_DET);
              var CENTRO_COSTO_DET = (selectedItem.CENTRO_COSTO_DET);
              var DIVISION = (selectedItem.DIVISION);
              var COD_COMPANIA = (selectedItem.COD_COMPANIA);  
              var COD_PAIS = (selectedItem.COD_PAIS);  
             
           
         $.ajax({
            type: "Get",
             data: { 'CUENTA_CONTABLE_ENC': CUENTA_CONTABLE_ENC,
                     'CUENTA_CONTABLE_DET': CUENTA_CONTABLE_DET, 
                     'CENTRO_COSTO_ENC': CENTRO_COSTO_ENC, 
                     'CENTRO_COSTO_DET': CENTRO_COSTO_DET, 
                     'DIVISION': DIVISION,
                     'COD_COMPANIA': COD_COMPANIA, 
                     'COD_PAIS': COD_PAIS  },
            url: "/api/Acount_Cuentas_API/Delete_HomCuentas",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            success: function (data) {
           debugger;
           if (data=='true') {

           var dataSourceReport = $("#gridHomCuentasContableAll").data("kendoGrid").dataSource;
            dataSourceReport.remove(dataItem);
            dataSourceReport.sync();  
               var ValorNuevo = CUENTA_CONTABLE_ENC + ' ' + CUENTA_CONTABLE_DET + ' ' + CENTRO_COSTO_ENC + ' ' + CENTRO_COSTO_DET + ' ' + DIVISION + ' ' + COD_COMPANIA + ' ' + COD_PAIS;

             Acount_Cuentas.Audit('INT_HOM_CUENTAS_CONTABLES',ValorNuevo,ValorNuevo,'DELETE') ;

              swal({
                    icon: "success",
                    text: "Cuenta Contable Eliminada Correctamente.",
                    button: "Aceptar"
                })

           }else{

              swal({
                    icon: "warning",
                    text: "Error al tratar de Eliminar la cuenta Contable.",
                    button: "Aceptar"
                })

           }
            
            },
            error: function (data) {


            }
        });

        } else {
            //Cuando se presiona el boton cancelar
        }
    });
         },
           //CREATE THE HOMOLOCACION DE TIPO DE DOCUMENTO...
     Create_HomCuentas: function (e) {
    swal({
        
        icon: "warning",
        text: "¿Esta seguro que deseas Guardarlo?",
        buttons: {
            cancel: "Cancelar",
            confirm: { text: "Aceptar", confirmButtonColor: '#044677' }
        },
        
    })
    .then((willDelete) => {
        if (willDelete) {
            
              var dataItem = this.dataItem($(e.target).closest("tr"));
              var grid = $("#gridHomCuentasContableAll").data("kendoGrid");
              var selectRow = $(e.currentTarget).closest("tr")
              var itemRowSelect = grid.dataItem(grid.select(selectRow))
              var grid2 = $("#gridHomCuentasContableAll").data("kendoGrid");
              var selectedItem = grid2.dataItem(grid2.select());

            var CUENTA_CONTABLE_ENC = (selectedItem.CUENTA_CONTABLE_ENC);
            var CENTRO_COSTO_ENC = (selectedItem.CENTRO_COSTO_ENC);
            var CUENTA_CONTABLE_DET = (selectedItem.CUENTA_CONTABLE_DET);
            var CENTRO_COSTO_DET = (selectedItem.CENTRO_COSTO_DET);
            var DIVISION = (selectedItem.DIVISION);
            var COD_COMPANIA = (selectedItem.COD_COMPANIA);  
            var COD_PAIS = (selectedItem.COD_PAIS);  
           
         $.ajax({
            type: "Get",
            data: { 'CUENTA_CONTABLE_ENC': CUENTA_CONTABLE_ENC,
                    'CUENTA_CONTABLE_DET': CUENTA_CONTABLE_DET, 
                    'CENTRO_COSTO_ENC': CENTRO_COSTO_ENC, 
                    'CENTRO_COSTO_DET': CENTRO_COSTO_DET, 
                    'DIVISION': DIVISION,
                    'COD_COMPANIA': COD_COMPANIA, 
                    'COD_PAIS': COD_PAIS  },
            url: "/api/Acount_Cuentas_API/Create_HomCuentas",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            success: function (data) {

             if (data=="Documento Creado Correctamente") {

                  swal({
                    icon: "success",
                    text: data,
                    button: "Aceptar"
                })

           var dataSourceReport = $("#gridHomCuentasContableAll").data("kendoGrid").dataSource;
             dataSourceReport.sync(); 

             var ValorNuevo = CUENTA_CONTABLE +' '+ CENTRO_COSTO+' '+DIVISION +' '+COD_COMPANIA+' '+COD_PAIS;

             Acount_Cuentas.Audit('INT_HOM_CUENTAS_CONTABLES','.',ValorNuevo,'CREACION') ;

             }else{

                swal({
                    icon: "warning",
                    text: data,
                    button: "Aceptar"
                })

           var dataSourceReport = $("#gridHomCuentasContableAll").data("kendoGrid").dataSource;
             dataSourceReport.sync();  
              Acount_Cuentas.HomCuentasGrid(); 
             }


            },
            error: function (data) {

             swal({
                    icon: "warning",
                    text: "Error al tratar de guardar la cuenta Contable.",
                    button: "Aceptar"
                })
            }
        });

        } else {

            //carga la tabla.
            Acount_Cuentas.HomCuentasGrid(); 
        }
    });
         },
          Audit: function (Tabla,ValorViejo,ValorNuevo,Evento) {
    
      var UserCarrousel = $("#user_carrousel").val();
         
        $.ajax({
            type: "Get",

             data:{ 'Tabla': Tabla ,'username': UserCarrousel ,'ValorViejo': ValorViejo ,'ValorNuevo': ValorNuevo,'Evento': Evento },
            url: "/api/Acount_API/Audit",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",
            success: function (data) {
          
                
            },
            error: function (data) {
            }
        });  
         }
};

$(document).ready(function () {
    Acount_Cuentas.init();
});
