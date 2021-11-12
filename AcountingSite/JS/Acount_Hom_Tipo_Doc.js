
var Acount_Tipo_DOC = {
    init: function () {
     
        //Acount_Tipo_DOC.HomTipeDocGrid();
    },

      //VIEW THE  HOMOLOCACION DE TIPO DE DOCUMENTO...
     HomTipeDocGrid: function () {
    
           var data = {
            'username': 'HOLA'
        };     
        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Acount_Tipo_DOC_API/GetHomTipeDocGrid",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",
            success: function (data) {
          
                $("#gridHomTipoDocAll").kendoGrid({
                    dataSource: data,
                    pageable: true,
                       filterable: {
                            mode: "row"
                        },
                    selectable: true,
                    columns: [
                        { field: "TIPO_DOC_EXACTUS", title: "Documento en Exactus", width: 200  },
                        { field: "TIPO_DOC_BAW", title: "Documento en BAW", width: 200 },
                        { field: "COD_PAIS", title: "Codigo de Pais",  width: 150 },
                        { field: "COD_COMPANIA", title: "Codigo de Compañia", width: 180 },
                         { command: { text: "", name: "Create", iconClass: "btn fas fa-save", click: Acount_Tipo_DOC.Create_HomTipeDoc }, title: "" , width: 60 },
                        { command: { text: "", name: "des", iconClass: "btn far fa-trash-alt", click: Acount_Tipo_DOC.Delete_HomTipeDoc }, title: "", width: 60 },
                        ],editable: "incell",
                       toolbar:[{name:"create", text: "Crear Nuevo DOC"}]
                 });
            },
            error: function (data) {
            }
        });  
    },

      //DELETE THE HOMOLOCACION DE TIPO DE DOCUMENTO...
     Delete_HomTipeDoc: function (e) {

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
              var grid = $("#gridHomTipoDocAll").data("kendoGrid");
              var selectRow = $(e.currentTarget).closest("tr")
              var itemRowSelect = grid.dataItem(grid.select(selectRow))
              var grid2 = $("#gridHomTipoDocAll").data("kendoGrid");
              var selectedItem = grid2.dataItem(grid2.select());

            var TIPO_DOC_EXACTUS = (selectedItem.TIPO_DOC_EXACTUS);
            var TIPO_DOC_BAW = (selectedItem.TIPO_DOC_BAW);
            var COD_PAIS = (selectedItem.COD_PAIS);
            var COD_COMPANIA = (selectedItem.COD_COMPANIA);  
            
         $.ajax({
            type: "Get",
             data: { 'TIPO_DOC_EXACTUS': TIPO_DOC_EXACTUS, 'TIPO_DOC_BAW': TIPO_DOC_BAW, 'COD_PAIS': COD_PAIS, 'COD_COMPANIA': COD_COMPANIA },
            url: "/api/Acount_Tipo_DOC_API/Delete_HomTipeDoc",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            success: function (data) {
           debugger;
           if (data=='true') {

           var dataSourceReport = $("#gridHomTipoDocAll").data("kendoGrid").dataSource;
            dataSourceReport.remove(dataItem);
            dataSourceReport.sync();  
               var ValorNuevo = TIPO_DOC_EXACTUS +' '+ TIPO_DOC_BAW+' '+COD_PAIS +' '+COD_COMPANIA;

             Acount_Tipo_DOC.Audit('INT_HOM_TIPO_DOC',ValorNuevo,ValorNuevo,'DELETE') ;

              swal({
                    icon: "success",
                    text: "Documento Eliminado Correctamente.",
                    button: "Aceptar"
                })

           }else{

              swal({
                    icon: "warning",
                    text: "Error al tratar de Eliminar el Documento.",
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
     Create_HomTipeDoc: function (e) {
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
              var grid = $("#gridHomTipoDocAll").data("kendoGrid");
              var selectRow = $(e.currentTarget).closest("tr")
              var itemRowSelect = grid.dataItem(grid.select(selectRow))
              var grid2 = $("#gridHomTipoDocAll").data("kendoGrid");
              var selectedItem = grid2.dataItem(grid2.select());

              var TIPO_DOC_EXACTUS = (selectedItem.TIPO_DOC_EXACTUS);
              var TIPO_DOC_BAW = (selectedItem.TIPO_DOC_BAW);  
              var COD_PAIS = (selectedItem.COD_PAIS);  
              var COD_COMPANIA = (selectedItem.COD_COMPANIA);      

         $.ajax({
            type: "Get",
            data:{ 'TIPO_DOC_EXACTUS': TIPO_DOC_EXACTUS ,'TIPO_DOC_BAW': TIPO_DOC_BAW ,'COD_PAIS': COD_PAIS ,'COD_COMPANIA': COD_COMPANIA },
            url: "/api/Acount_Tipo_DOC_API/Create_HomTipeDoc",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            success: function (data) {

             if (data=="Documento Creado Correctamente") {

                  swal({
                    icon: "success",
                    text: data,
                    button: "Aceptar"
                })

           var dataSourceReport = $("#gridHomTipoDocAll").data("kendoGrid").dataSource;
             dataSourceReport.sync(); 

             var ValorNuevo = TIPO_DOC_EXACTUS +' '+ TIPO_DOC_BAW+' '+COD_PAIS +' '+COD_COMPANIA;

             Acount_Tipo_DOC.Audit('INT_HOM_TIPO_DOC','.',ValorNuevo,'CREACION') ;

             }else{

                swal({
                    icon: "warning",
                    text: data,
                    button: "Aceptar"
                })

           var dataSourceReport = $("#gridHomTipoDocAll").data("kendoGrid").dataSource;
             dataSourceReport.sync();  
              Acount_Tipo_DOC.HomTipeDocGrid(); 
             }


            },
            error: function (data) {

             swal({
                    icon: "warning",
                    text: "Error al tratar de guardar el Documento.",
                    button: "Aceptar"
                })
            }
        });

        } else {

            //carga la tabla.
            Acount_Tipo_DOC.HomTipeDocGrid(); 
        }
    });
         },         
          validation: function (Tabla,ValorViejo,ValorNuevo,Evento) {
    
    
       

             //data:{ 'Tabla': Tabla ,'username': UserCarrousel ,'ValorViejo': ValorViejo ,'ValorNuevo': ValorNuevo,'Evento': Evento, },
          
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
    Acount_Tipo_DOC.init();
});
