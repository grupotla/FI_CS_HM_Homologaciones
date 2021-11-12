
var data_tempo;

var btnAsociar = "<button onclick='AsociarRegistros(0)' title='ASOCIAR / HOMOLOGAR'><span class='k-icon k-i-link-vertical colored-green' size='large'></span></button>";

//var css = "#=data.IERPH_ACTIVO == 'S' ? '' : (data.IERPH_ACTIVO == 'N' ? 'style=color:silver' : 'style=color:navy')#";
var css = "#=data.IERPH_ACTIVO != 'S' ? 'style=color:gray' : (data.IERPH_ID != '0' ? 'style=color:blue' : (data.IERPH_STATUS != '1' ? 'style=color:red' : ''))#";

var windowTemplate = kendo.template($("#windowTemplate").html());

var window2 = $("#window2").kendoWindow({
    title: "DATOS GENERALES",
    visible: false, //the window will not appear before its .open method is called
    width: "500px",
    height: "300px",
}).data("kendoWindow");

function windowTemplateClick(target,e) {  //add a click event listener on the delete button

    try {

        //e.preventDefault(); //prevent page scroll reset
        //var tr = $(e.target).closest("tr"); //get the row for deletion

        var tr = $(target).closest("tr"); //get the row for deletion
        var data = e.dataItem(tr); //get the row data so it can be referred later

        //debugger

        if (data.IERPH_STATUS != 3 && data.IERPH_STATUS != "3") {

            window2.content(windowTemplate(data)); //send the row data object to the template and render it
            window2.center().open();

            console.log(data);

            console.log($("#window2_wnd_title"));

            //$("#window2_wnd_title").html("DATOS GENERALES " + (data.IERPH_SYSTEMA == "ERP" ? data.IERPH_ERP_CODIGO + " EXACTUS" : data.IERPH_CODIGO + " OPERATIVO") );

            $("#window2_wnd_title").html("" + (data.IERPH_SYSTEMA == "ERP" ? "EXACTUS " + data.IERPH_ERP_CODIGO : "OPERATIVO " + data.IERPH_CODIGO ));

            $("#yesButton").click(function () {
                //grid.dataSource.remove(data)  //prepare a "destroy" request
                //grid.dataSource.sync()  //actually send the request (might be ommited if the autoSync option is enabled in the dataSource)
                window2.close();
            })
            $("#noButton").click(function () {
                window2.close();
            })
        }

    }
    catch (err) {

        console.log(err);
    }




}



function onDataBound(e) {

    var data = this.dataSource.view();
    var grid = e.sender.element.context.id;
    var dato;
    var td;

    for (var i = 0; i < data.length; i++) {
        var uid = data[i].uid;
        var row = this.table.find("tr[data-uid='" + uid + "']");

        var css2 = data[i].IERPH_ACTIVO != 'S' ? 'style=color:gray' : (data[i].IERPH_ID != '0' ? 'style=color:blue' : (data[i].IERPH_STATUS != '1' ? 'style=color:red' : ''));


        if (data[i].IERPH_STATUS != 3 && data[i].IERPH_STATUS != "3") {

            if (grid == "gridHomHomologaciones") {
                if (data[i].IERPH_CODIGO) {
                    dato = data[i].IERPH_CODIGO;
                }
            }

            if (grid == "gridHomHomologacionesERP") {
                if (data[i].IERPH_ERP_CODIGO) {
                    dato = data[i].IERPH_ERP_CODIGO;
                    if (data[i].IERPH_TIPO_MONEDA != "")
                        dato += " (" + data[i].IERPH_TIPO_MONEDA + ")";
                }
            }

            //        $(row).find("td:eq(2)").text('');

            td = row.find(".k-command-cell").contents();

            td[0].innerHTML = '<span class="k-icon k-i-info colored-blue" ' + css2 + ' ></span><span ' + css2 + ' style="width:90%" title="' + dato + '"> ' + dato + '</span>';

            console.log(td);
        }
    }
}




var Catalogos_Calls = {
    init: function () {


    },

    GetTest: function () {

        $.ajax({
            type: "Get",
            //data: data,
            url: "/api/Catalogos_Calls_API/GetTest",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {


            },
            success: function (data) {

                alert(data);

            },
            error: function (e) {
                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })
            }

        }).done(function (result) {

        }).always(function () {

        });

    },


    GetRolUser: function (user) {

        $.ajax({
            type: "Get",
            data: {usuario:user},
            url: "/api/Catalogos_Calls_API/GetRolUser",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {


            },
            success: function (data) {

                //console.log('1-------------');

                //console.log('user : ' + user);
                //console.log($("#user_carrousel").val());
                //console.log($("#user_rol").val());

                var user = data.data1 ? data.data1 : $("#user_carrousel").val();
                var rol = data.data2 ? data.data2 : $("#user_rol").val();
       

                //console.log('2-------------');

                $("#user_carrousel").val(user);
                $("#user_rol").val(rol);
     

                
                //console.log(data);
                //console.log(rol);

                SetRol(rol, data.data3);

            },
            error: function (e) {
                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })
            }

        }).done(function (result) {

        }).always(function () {

        });
    },



    GetCategoriasIB: function (catalogo) {


        var data = {
            'IERPC_ID': catalogo,
        };

        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Catalogos_Calls_API/GetCategorias",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {
                $("#cate_cs_id").val("");
                $("#cate_cs_ds").val("");
                $("#cate_ex_id").val("");
                $("#cate_ex_ds").val("");
            },

            success: function (data) {
                $("#cate_cs_id").val(data.data1);
                $("#cate_cs_ds").val(data.data2);
                $("#cate_ex_id").val(data.data3);
                $("#cate_ex_ds").val(data.data4);
            },

            error: function (e) {
                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })
            }

        }).done(function (result) {

        }).always(function () {

        });

    },








    //// 2021-06-07 se agrego data array para llevar el select de empresas para el admin de paises
    GetModuleSelect: function () {


        var data = {
            'usuario': document.getElementById('user_carrousel') ? document.getElementById('user_carrousel').value : '',
        };

        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Catalogos_Calls_API/GetModuleSelect",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {

                if ($("#HomCatalogosMenu").is(":visible"))
                    window.kendo.ui.progress($("#gridHomCatalogosAlls"), true);

                if ($("#HomHomologacionesMenu").is(":visible")) {
                    $("#gridHomHomologaciones").html('<BR><BR><BR><BR><BR><BR><BR><BR>');
                    $("#gridHomHomologacionesERP").html('<BR><BR><BR><BR><BR><BR><BR><BR>');  
                    window.kendo.ui.progress($("#gridHomHomologaciones"), true);
                    window.kendo.ui.progress($("#gridHomHomologacionesERP"), true);
                }

            },

            success: function (data) {

                


                if ($("#HomCatalogosMenu").is(":visible")) { // CATALOGOS

                    $("#IERPM_ID").html(data[0]);

                    if ($("#HomCatalogosLabel").html() == "ADMIN PAISES" || $("#HomCatalogosLabel").html() == "ADMIN USUARIOS" || $("#HomCatalogosLabel").html() == "ADMIN CENTRO COSTO")
                        if ($("#IERPE_ID"))
                            $("#IERPE_ID").html(data[1]);

                    $("#IERPE_ID").focus();
                }

                if ($("#HomHomologacionesMenu").is(":visible")) { // HOMOLOGACIONES

                    $("#IERPHM_ID").html(data[0]);

                    $("#gridHomHomologaciones").html('');
                    $("#gridHomHomologacionesERP").html('');

                    $("#IERPHE_ID").html(data[1]);

                    $("#IERPHE_ID").focus();

                }

                
              
            },

            error: function (e) {
                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })
            }

        }).done(function (result) {

        }).always(function () {
            if ($("#HomCatalogosMenu").is(":visible"))
            window.kendo.ui.progress($("#gridHomCatalogosAlls"), false);
            if ($("#HomHomologacionesMenu").is(":visible")) {
                window.kendo.ui.progress($("#gridHomHomologaciones"), false);
                window.kendo.ui.progress($("#gridHomHomologacionesERP"), false);
            }
        });

    },





    Get_IB_CATALOGOS: function (data) {

        var IERPM_ID = data.IERPM_ID;
        var IERPM_DES = data.IERPM_DES;
        var IERP_OPCION = data.IERP_OPCION;

        var data_tempo = data;

        var esconde, esconde2, ierp_titulo = "Titulo del Catalogo"; 
        var cscod; 
        var excod; 

        esconde2 = false;

        switch (data.IERP_OPCION) {
            case "ADMIN CATALOGOS":
                esconde = false;
                cscod = "Cod Catalogo Operativo";
                excod = "Cod Catalogo ERP";
                break;
            case "ADMIN PAISES":
                esconde = true;
                cscod = "Cod Pais Operativo";
                excod = "Cod Pais ERP";
                break;
            case "ADMIN EMPRESAS":
                esconde = true;
                cscod = "Cod Empresa Operativo";
                excod = "Cod Empresa ERP";
                break;
            case "ADMIN RUBROS":
                esconde = true;
                cscod = "Cod Rubro Operativo";
                excod = "Cod Rubro ERP";
                break;
            case "ADMIN MONEDAS":
                esconde = true;
                cscod = "Cod Moneda Operativo";
                excod = "Cod Moneda ERP";
                break;
            case "ADMIN CATEGORIAS":
                esconde = false;
                esconde2 = false;
                cscod = "Sistema";
                excod = "Cod Categoria";
                ierp_titulo = "Descripcion de Categoria";
                break;
            case "ADMIN QUERYS":
                esconde = false;
                esconde2 = false;
                cscod = "Sistema - Cat - Tabla";
                excod = "Ordenamiento";
                ierp_titulo = "Sql";
                break;
            case "ADMIN FILTROS":
                esconde = false;
                esconde2 = false;
                ierp_titulo = "Sistema - Cat - Tabla";
                cscod = "Operador - Campo a Filtrar";
                excod = "Comparativo";
                break;

            case "ADMIN USUARIOS":
                esconde = true;
                cscod = "Usuario";
                excod = "Roll";
                break;

            case "ADMIN CENTRO COSTO":
                esconde = false;
                ierp_titulo = "Servicio CS Descripcion";
                cscod = "Servicio CS";
                excod = "Centro Costo ERP";
                break;

        }

        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Catalogos_Calls_API/Get_IB_CATALOGOS",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {
                window.kendo.ui.progress($("#gridHomCatalogosAlls"), true);
            },

            success: function (data) {

                var data_grid = data[0];

                if (IERP_OPCION == "ADMIN CATALOGOS" || IERP_OPCION == "ADMIN PAISES" || IERP_OPCION == "ADMIN EMPRESAS") {

                    var data_select1 = data[1];
                    var data_select2 = data[2];

                    $("#IERP_CODIGO_HTML").html(data_select1[0].IERP_TITULO);
                    $("#IERP_ERP_CODIGO_HTML").html(data_select2[0].IERP_TITULO);
                }



                if (IERP_OPCION == "ADMIN USUARIOS") {

                    //var IERP_ERP_CODIGO = $("#IERP_TITULO").val();

                    //var data_select1 = data[1];

                    //$("#IERP_ERP_CODIGO_HTML").html(data_select1[0].IERP_TITULO);


                    var data_select1 = data[1];
                    var data_select2 = data[2];

                    $("#IERP_CODIGO_HTML").html(data_select1[0].IERP_TITULO);
                    $("#IERP_ERP_CODIGO_HTML").html(data_select2[0].IERP_TITULO);


                }



                if (IERP_OPCION == "ADMIN CENTRO COSTO") {

                    var data_select1 = data[1];
                    $("#IERP_CODIGO_HTML").html(data_select1[0].IERP_TITULO);
                }


                if (IERP_OPCION == "ADMIN CATEGORIAS" || IERP_OPCION == "ADMIN QUERYS" ) {

                        var IERP_ERP_CODIGO = $("#IERP_ERP_CODIGO").val();
                    
                        //$("#IERP_ERP_CODIGO_HTML").html('<select id="IERP_ERP_CODIGO" required ' + (IERP_OPCION == "ADMIN CATEGORIAS" ? 'onchange = "CategoriasDescripcion()"' : '') + ' class="HeaderFilters"><option value="">Seleccione</option><option value="ERP">ERP</option><option value="' + IERPM_DES + '">' + IERPM_DES + '</option></select>');

                        $("#IERP_ERP_CODIGO_HTML").html('<select id="IERP_ERP_CODIGO" required onchange = "CategoriasDescripcion()" class="HeaderFilters"><option value="">Seleccione</option><option value="ERP">ERP</option><option value="' + IERPM_DES + '">' + IERPM_DES + '</option></select><input type=checkbox id="IERP_ERP_CODIGO_CHK"> Filtrar ');   

                        $("#IERP_ERP_CODIGO").val(IERP_ERP_CODIGO); //restablece el valor anterior
                   
                }

                if (IERP_OPCION == "ADMIN FILTROS") {

                    var IERP_ERP_CODIGO = $("#IERP_TITULO").val();

                    var data_select1 = data[1];

                    //$("#IERP_TITULO_HTML").html(data_select1[0].IERP_TITULO);

                    $("#IERP_TITULO").html(data_select1[0].IERP_TITULO);

                    $("#IERP_TITULO").val(data_tempo.IERP_ERP_CODIGO); //restablece el valor anterior

                }




                data = data_grid;


                $("#gridHomCatalogosAlls").kendoGrid({
                    dataSource: { data, pageSize: 10 },
                    pageable: {
                        alwaysVisible: true,
                        pageSizes: [10, 20]
                    },
                    columns: [
                        { field: "Edit", "template": '<input type="button"  alt="Editar" value="#=data.IERP_ID#" onclick="Catalogos_Calls.Edit_IB_CATALOGOS(\'#=data.IERP_ID#\')" />', width: 30 },
                        //{ command: { text: "Edit", name: "Edit", click: Catalogos_Calls.Edit_IB_CATALOGOS }, title: "", iconClass: "btn fas fl-edit", width: 30 },
                        { field: "IERP_CODIGO", title: cscod, width: 80 },
                        { field: "IERP_ERP_CODIGO", title: excod, width: 80, hidden: esconde2  },
                        { field: "IERP_TITULO", title: ierp_titulo, width: 100, hidden: esconde },
                        { field: "IERP_USER", title: "User Crea", width: 40 },
                        { field: "IERP_DATE", title: "Fecha Crea", width: 70 },
                        { field: "IERP_STATUS", title: "Estado", "template": '<span alt="#=data.IERP_STATUS#" title="#=data.IERP_STATUS#">#=data.IERP_STATUS#</span>', width: 30 },
                    ],
                });
            },
            error: function (e) {
                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })
            }

        }).done(function (result) {

        }).always(function () {
            window.kendo.ui.progress($("#gridHomCatalogosAlls"), false);
        });

    },


    Save_IB_CATALOGOS: function (data) {
    
        if ($("#TbLoader").val() == '1')
            return false;


        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Catalogos_Calls_API/Save_IB_CATALOGOS",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {
                $("#TbLoader").val("1");
                window.kendo.ui.progress($("#gridHomCatalogosAlls"), true);
            },

            success: function (data) {

                

                if (data == "Exito al Guardar" || data == "Exito al Borrar") {

                    swal({
                        icon: "success",
                        text: data,
                        button: "Aceptar"
                    })

                } else {

                    swal({
                        icon: "warning",
                        text: data,
                        button: "Aceptar"
                    })
                }

            },
            error: function (e) {
                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })
            }

        }).done(function (result) {

            CleanCatalogos();

            data.IERP_CHK = $("#IERPM_CHK").prop("checked") ? '1' : '0';

            Catalogos_Calls.Get_IB_CATALOGOS(data);

        }).always(function () {
            //window.kendo.ui.progress($("#gridHomCatalogosAlls"), false);
        });

 
    },


    Edit_IB_CATALOGOS: function (IERP_ID) {

        if ($("#TbLoader").val() == '1')
            return false;

        var data = {
            'IERP_ID': IERP_ID, //selectedItem.IERP_ID,
            'IERP_OPCION': $("#HomCatalogosLabel").html()
        };
   
        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Catalogos_Calls_API/Edit_IB_CATALOGOS",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {
                $("#TbLoader").val("1");
                window.kendo.ui.progress($("#gridHomCatalogosAlls"), true);
            },

            success: function (data) {

                
      
                if (data[0].IERP_ID != null) {

                    if (data[0].IERP_STATUS == '1') {

                      
                        $("#IERP_ID").val(data[0].IERP_ID);

                        if ($("#HomCatalogosLabel").html() != "ADMIN QUERYS" && $("#HomCatalogosLabel").html() != "ADMIN FILTROS" && $("#HomCatalogosLabel").html() != "ADMIN USUARIOS")
                            $("#IERPM_ID").val(data[0].IERPM_ID);

                        $("#IERP_TITULO").val(data[0].IERP_TITULO);

                        if ($("#HomCatalogosLabel").html() == "ADMIN USUARIOS")
                            $("#IERP_CODIGO").val(data[0].IERP_ID + "|" + data[0].IERP_CODIGO + "|" + data[0].IERP_TITULO);
                        else
                            $("#IERP_CODIGO").val(data[0].IERP_CODIGO);

                        $("#IERP_ERP_CODIGO").val(data[0].IERP_ERP_CODIGO);
                        $("#IERPS_SEC").val(data[0].IERPS_SEC);

                        if (document.getElementById('IERP_CAMPO_PROGRA'))
                            document.getElementById('IERP_CAMPO_PROGRA').value = data[0].IERP_CAMPO_PROGRA;

                        if (document.getElementById('IERP_ORDEN'))
                            document.getElementById('IERP_ORDEN').value = data[0].IERP_ORDEN;

                        if (document.getElementById('IERP_SQL'))
                            document.getElementById('IERP_SQL').value = data[0].IERP_SQL.replace(/\'/g,"''");

                        if (document.getElementById('IERP_OPERADOR'))
                            document.getElementById('IERP_OPERADOR').value = data[0].IERP_OPERADOR;
                    } else {

                        swal({
                            icon: "error",
                            text: data[0].IERP_STATUS == "2" ? "Registro de bitacora" : "Registro fue borrado",
                            button: "Aceptar"
                        })
                    }

                } else {

                    swal({
                        icon: "error",
                        text: "Error en operacion",
                        button: "Aceptar"
                    })
                }


            },
            error: function (e) {
                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })
            }

        }).done(function (result) {



        }).always(function () {
            $("#TbLoader").val("");
            window.kendo.ui.progress($("#gridHomCatalogosAlls"), false);
        });


    },


    /////////////////////////////////77 2021-04-27 homologacion nueva para SC

    Get_IB_HOMOLOGACIONES: function (data) {


        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Catalogos_Calls_API/Get_IB_HOMOLOGACIONES",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {

                $("#gridHomHomologaciones").html('<BR><BR><BR><BR><BR><BR><BR><BR>');
                $("#gridHomHomologacionesERP").html('<BR><BR><BR><BR><BR><BR><BR><BR>');  

                window.kendo.ui.progress($("#gridHomHomologaciones"), true);
                window.kendo.ui.progress($("#gridHomHomologacionesERP"), true);
            },

            success: function (data) {

                $("#gridHomHomologaciones").html(''); 
                $("#gridHomHomologacionesERP").html(''); 

                $("#IERPHC_ID").html(data[0]);   //catalogos

                $("#IERPHP_ID").html(data[1]);  //paises

                $("#IERPM_SQL_CS").val(data[2]);

                // este combo se llena desde el principio junto con modulos
                //$("#IERPHE_ID").html(data[2]);  //empresas
                
            },
            error: function (e) {
                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })
            }

        }).done(function (result) {

        }).always(function () {
            window.kendo.ui.progress($("#gridHomHomologaciones"), false);
            window.kendo.ui.progress($("#gridHomHomologacionesERP"), false);
        });

    },


    Desasociar: function (cual, cod_uno) {

        var check = $("#" + cual + cod_uno);

        var row = $(check).closest("tr");

        var cod_dos = $(row).find("td:eq(2)").text();

        var CDID = cual == "CS" ? cod_uno : cod_dos;
        var EXID = cual == "EX" ? cod_uno : cod_dos;

        var IERPH_ID, str;

        try {

            IERPH_ID = $(check).val();
        }
        catch (err) {

            str = err.message;
        }


        if ($(check).prop("checked") == true && $(check).prop("disabled")) {

            data_tempo.IERPH_ID = IERPH_ID;
            data_tempo.CDID = CDID;
            data_tempo.CDIDStr = '';
            data_tempo.EXID = EXID;
            data_tempo.EXIDStr = '';
            data_tempo.IERPH_TIPO_MONEDA = '';
            data_tempo.NIT = '';



            Catalogos_Calls.HomHomologacionDesasociar(data_tempo, row);

        } else {

            if ($(check).prop("checked") == true)
                AsociarRegistros(1);

        }

        return false;

    },


    Set_TipoMoneda: function () {

        $.ajax({
            type: "Post",
            url: "/api/Catalogos_Calls_API/Set_TipoMoneda",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {

            },

            success: function (data) {

                $("#IERPH_TIPO_MONEDA").html(data);

            },
            error: function (e) {

                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })

            }

        }).done(function (result) {

        }).always(function () {

        });

    },


    SubmitCombos: function () {

        if ($("#TbLoader").val() == '1')
            return false;

  

        $("#TbLoader").val('1');

        $("#TbAsociar").hide();

        CleanHomologaciones2();

        var user_carrousel = document.getElementById('user_carrousel').value;


        var data = {
            'OPE1': 'DEL',  //necesario cuando se quiere desasociar este dato va a la funcion
            'IERPH_STATUS': '3',
            'IERPHM_ID': document.getElementById('IERPHM_ID').value,
            'IERPHM_DES': $("#IERPHM_ID option:selected").text(),
            'IERPHC_ID': document.getElementById('IERPHC_ID').value,
            'IERPHP_ID': document.getElementById('IERPHP_ID').value,
            'IERPHE_ID': document.getElementById('IERPHE_ID').value,
            'IERPH_NOMBRE': document.getElementById('IERPH_NOMBRE').value,
            'IERPH_NIT': document.getElementById('IERPH_NIT').value,
            'usuario': user_carrousel,
            'IERPH_OPCION': $("#HomHomologacionesLabel").html(),
            'IERPHM_CHK': $("#IERPHM_CHK").prop("checked") ? '1' : '0',
            'IERPH_TITLE': '',
        };

        if ($("#HomoCS").prop("checked") == true)
            Catalogos_Calls.Set_IB_HOMOLOGACION(data);

        if ($("#HomoERP").prop("checked") == true)
            Catalogos_Calls.Set_IB_HOMOLOGACION_ERP(data);


        $("#TbLoader").val('');

        if (data.IERPH_OPCION == 'OPERACION DE HOMOLOGACIONES')
            $("#TbAsociar").show();

        $("#IERPHM_ID2").val($("#IERPHM_ID").val());
        $("#IERPHC_ID2").val($("#IERPHC_ID").val());
        $("#IERPHP_ID2").val($("#IERPHP_ID").val());
        $("#IERPHE_ID2").val($("#IERPHE_ID").val());

        $("#IERPH_NOMBRE").val('');
        $("#IERPH_NIT").val('');



    },


    Set_IB_HOMOLOGACION: function (data) {

        data_tempo = data;

        //var homo_saved;



        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Catalogos_Calls_API/Set_IB_HOMOLOGACION",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {

                $("#gridHomHomologaciones").html('<BR><BR><BR><BR><BR><BR><BR><BR>');
                //$("#gridHomHomologacionesERP").html('<BR><BR><BR><BR><BR><BR><BR><BR>');  

                window.kendo.ui.progress($("#gridHomHomologaciones"), true);
                //window.kendo.ui.progress($("#gridHomHomologacionesERP"), true);
            },

            success: function (data) {

                $("#gridHomHomologaciones").html('');

                //homo_saved = data[1]; // homologaciones saved

                var tempo = data[1];

                data = data[0]; // data grid

                var IERPH_NOMBRE = 'CS NOMBRE';

                var tem1 = tempo[0];

                //data_tempo.IERPH_TITLE = tem1.IERPH_ERP_NOMBRE;
                IERPH_NOMBRE = tem1.IERPH_NOMBRE;

                if (data.length == 0)
                    $("#gridHomHomologaciones").html('Busqueda Operativo "' + data_tempo.IERPH_NOMBRE + '" sin resultados');
                else

                $("#gridHomHomologaciones").kendoGrid({
                    dataSource: { data, pageSize: 10 },
                    pageable: true,
                    /*
                    pageable: {
                        alwaysVisible: true,
                        pageSizes: [10, 20]
                    },*/
                    dataBound: onDataBound,
                    filterable: {
                        mode: "row"
                    },

                    /*
                    selectable: true,
                    change: function (e) {
                        var selectedRow = this.select();
                        windowTemplateClick(selectedRow,this);
                    },
                    */
                    columns: [

                        { field: "IERPH_HOMOLOGADO", "template": '<input type="checkbox" id="CS#=data.IERPH_CODIGO#" name=CS[] value="#=data.IERPH_ID#" #=data.IERPH_ID > 0 ? "checked=checked" : ""#  #=data.IERPH_ID > 0 || data.IERPH_ACTIVO != "S" || data.IERPH_STATUS != "1"  ? "disabled=true" : ""#  onclick=CheckBoxClicked(this) />', title: "<span title='HOMOLOGADO' >" + "H" + "</span>", width: 30 },
                        { field: "IERPH_CODIGO", hidden: true },
                        { field: "IERPH_ERP_CODIGO", hidden: true },
                        { field: "IERPH_NOMBRE", hidden: true },
                        {
                            field: "IERPH_CODIGO2", title: "CS CODE", width: 60, 
                            command: {
                                name: "command",
                                text: "",
                                click: function (e) {  //add a click event listener on the delete button
                                    e.preventDefault(); //prevent page scroll reset
                                    windowTemplateClick(e.target, this);
                                }
                            },                                                    
                        },

                        { width: 60, field: "IERPH_ERP_CODIGO2", "template": "<button #=data.IERPH_ERP_CODIGO == \"\" && data.IERPH_STATUS == \"1\" ? \"\" : \"style='display:none'\"# onclick='AsociarRegistros(0)' title=\"ASOCIAR / HOMOLOGAR\" ><span class=\"k-icon k-i-link-vertical colored-green\" size=\"large\"></span></button><button #=data.IERPH_ERP_CODIGO == \"\" ? \"style='display:none'\" : \"\"# title=\"DES-ASOCIAR : #=data.IERPH_ERP_CODIGO# #=data.IERPH_ERP_NOMBRE#\" id=\"CSBT#=data.IERPH_CODIGO#\"  #=data.IERPH_ID == \"0\" || data.IERPH_STATUS != \"1\" || data.IERPH_ACTIVO != \"S\" ? \"disabled=true\" : \"\"# onclick=\"return Catalogos_Calls.Desasociar(\'CS\',\'#=data.IERPH_CODIGO#\')\"><span class=\"k-icon k-i-unlink-vertical colored-red\" size=\"large\"></span><span " + css + ">&nbsp;#=data.IERPH_ERP_CODIGO2# #=data.IERPH_TIPO_MONEDA#</span></button>", title: "HOMOLOGADO" },

                        //{"template": "<button title=\"#=data.IERPH_ERP_CODIGO# #=data.IERPH_ERP_NOMBRE#\" ><span class=\"k-icon k-i-link-vertical colored-green\" size=\"large\"></span></button>", title: "<span title='' >" + "ERP" + "</span>", width: 30 },
                        //{"template": "<button title=\"#=data.IERPH_ERP_CODIGO# #=data.IERPH_ERP_NOMBRE#\" id=\"CSBT#=data.IERPH_CODIGO#\"  #=data.IERPH_ID == \"0\" || data.IERPH_STATUS != \"1\" || data.IERPH_ACTIVO != \"S\" ? \"disabled=true\" : \"\"# onclick=\"Catalogos_Calls.Desasociar(\'CS\',\'#=data.IERPH_CODIGO#\')\"><span class=\"k-icon k-i-unlink-vertical colored-red\" size=\"large\"></span> #=data.IERPH_ERP_CODIGO2#</button>", title: "<span title=''>" + "ERP" + "</span>", width: 30 },


                        { width: 90, field: "IERPH_NOMBRE", "template": "<span " + css + ">#=data.IERPH_NOMBRE#</span>", title: "<span title='" + IERPH_NOMBRE + "'>" + IERPH_NOMBRE + "</span>" },


                    ], 
 
                });






            },
            error: function (e) {

                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })

          
            }

        }).done(function (result) {

            //Catalogos_Calls.Set_IB_HOMOLOGACION_ERP(data_tempo);

        }).always(function () {
             window.kendo.ui.progress($("#gridHomHomologaciones"), false);

        });


    },


    Set_IB_HOMOLOGACION_ERP: function (data) {


        data_tempo = data;

        //var IERPH_ERP_NOMBRE = 'ERP NOMBRE';

        var IERPHC_ID = data.IERPHC_ID;

        //if (data.IERPH_TITLE)
        //    IERPH_ERP_NOMBRE = data.IERPH_TITLE;

        //debugger
        //data.homs = data_homo;
        

        $.ajax({
            type: "Get",
            data: data,
            url: "/api/Catalogos_Calls_API/Set_IB_HOMOLOGACION_ERP",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            culture: "es-ES",

            beforeSend: function () {


                //$("#gridHomHomologaciones").html('<BR><BR><BR><BR><BR><BR><BR><BR>');
                $("#gridHomHomologacionesERP").html('<BR><BR><BR><BR><BR><BR><BR><BR>');

                //window.kendo.ui.progress($("#gridHomHomologaciones"), true);
                window.kendo.ui.progress($("#gridHomHomologacionesERP"), true);

            },

            success: function (data) {

                console.log(data);

                var tempo = data[1];

                data = data[0]; // data grid

                var IERPH_ERP_NOMBRE = 'ERP NOMBRE';

                var tem1 = tempo[0];

                //data_tempo.IERPH_TITLE = tem1.IERPH_ERP_NOMBRE;
                IERPH_ERP_NOMBRE = tem1.IERPH_ERP_NOMBRE;

                $("#gridHomHomologacionesERP").html('');

                //console.log(data);

                

                if (data.length == 0)
                    $("#gridHomHomologacionesERP").html('Busqueda ERP "' + (data_tempo.IERPH_NIT == "" && data_tempo.IERPH_NOMBRE != "" ? data_tempo.IERPH_NOMBRE : data_tempo.IERPH_NIT)  + '" sin resultados'); 


                else

                $("#gridHomHomologacionesERP").kendoGrid({
                    dataSource: {
                        data, pageSize: 10,
                        filter: { field: "IERPH_ACTIVO", operator: "eq", value: $("#HomHomologacionesLabel").html() == 'CONSULTA DE HOMOLOGACIONES' ? "C" :  "S" }
                    },
                    pageable: {
                        alwaysVisible: true,
                        pageSizes: [10, 20]
                    },
                    dataBound: onDataBound,
                    filterable: {
                        mode: "row"
                    },
                    /* selectable: true,
                    change: function (e) {
                        var selectedRow = this.select();
                        windowTemplateClick(selectedRow, this);
                    }, */
                    columns: [
                     
                        //{ field: "IERPH_HOMOLOGADO", "template": IERPHC_ID == "321" ? '<input type="checkbox" id="EX#=data.IERPH_ERP_CODIGO#" name=EX[] value="#=data.IERPH_ID#" onclick=CheckBoxClicked(this) style="margin:0px;" />' : '<input type="checkbox" id="EX#=data.IERPH_ERP_CODIGO#" name=EX[] value="#=data.IERPH_ID#" #=data.IERPH_ID > 0 ? "checked=checked" : ""#  #=data.IERPH_ID > 0 || data.IERPH_ACTIVO != "S" || data.IERPH_STATUS != "1" ? "disabled=true" : ""#  onclick=CheckBoxClicked(this) />', title: "<span title='HOMOLOGADO' >" + "H" + "</span>", width: 30 },
                        { field: "IERPH_HOMOLOGADO", "template": '<input type="checkbox" id="EX#=data.IERPH_ERP_CODIGO#" name=EX[] value="#=data.IERPH_ID#" #=data.IERPH_ID > 0 ? "checked=checked" : ""#  #=data.IERPH_ID > 0 || data.IERPH_ACTIVO != "S" || data.IERPH_STATUS != "1" ? "disabled=true" : ""#  onclick=CheckBoxClicked(this) />', title: "<span title='HOMOLOGADO' >" + "H" + "</span>", width: 30 },
                        { field: "IERPH_ERP_CODIGO", hidden: true },
                        { field: "IERPH_CODIGO", hidden: true },
                        { field: "IERPH_ERP_NOMBRE", hidden: true },
                        {
                            field: "IERPH_ERP_CODIGO2", title: "ERP CODE", width: 60, 
                            command: {
                                name: "command",
                                text: "",
                                click: function (e) {  //add a click event listener on the delete button
                                    e.preventDefault(); //prevent page scroll reset
                                    windowTemplateClick(e.target, this);
                                }
                            },                                                    
                        },

                        //{ field: "IERPH_CODIGO2", "template": $("#user_rol").val() == "0" ? "" : "<button #=data.IERPH_ID != \"0\" || data.IERPH_STATUS != \"1\" || data.IERPH_ACTIVO == \"N\"  ? \"style='display:none'\" : \"\"# onclick='AsociarRegistros(0)' title=\"ASOCIAR / HOMOLOGAR\" ><span class=\"k-icon k-i-link-vertical colored-green\" size=\"large\"></span></button><button #=data.IERPH_CODIGO == \"\" ? \"style='display:none'\" : \"\"# title=\"DES-ASOCIAR : #=data.IERPH_CODIGO# #=data.IERPH_NOMBRE#\" id=\"EXBT#=data.IERPH_ERP_CODIGO#\"  #=data.IERPH_ID == \"0\" || data.IERPH_STATUS != \"1\" || data.IERPH_ACTIVO != \"S\" ? \"disabled=true\" : \"\"# onclick=\"return Catalogos_Calls.Desasociar(\'EX\',\'#=data.IERPH_ERP_CODIGO#\')\"><span class=\"k-icon k-i-unlink-vertical colored-red\" size=\"large\"></span><span " + css + ">&nbsp;#=data.IERPH_CODIGO2#</span></button>", title: "HOMOLOGADO", width: 40 },

                        { width: 60, field: "IERPH_CODIGO2", "template": "<button #=data.IERPH_CODIGO == \"\" && data.IERPH_STATUS == \"1\" ? \"\" : \"style='display:none'\"# onclick='AsociarRegistros(0)' title=\"ASOCIAR / HOMOLOGAR\" ><span class=\"k-icon k-i-link-vertical colored-green\" size=\"large\"></span></button><button #=data.IERPH_CODIGO == \"\" ? \"style='display:none'\" : \"\"# title=\"DES-ASOCIAR : #=data.IERPH_CODIGO# #=data.IERPH_NOMBRE#\" id=\"EXBT#=data.IERPH_ERP_CODIGO#\"  #=data.IERPH_ID == \"0\" || data.IERPH_STATUS != \"1\" || data.IERPH_ACTIVO != \"S\" ? \"disabled=true\" : \"\"# onclick=\"return Catalogos_Calls.Desasociar(\'EX\',\'#=data.IERPH_ERP_CODIGO#\')\"><span class=\"k-icon k-i-unlink-vertical colored-red\" size=\"large\"></span><span " + css + ">&nbsp;#=data.IERPH_CODIGO2#</span></button>", title: "HOMOLOGADO" },

                        { width: 90, field: "IERPH_ERP_NOMBRE", "template": "<span " + css + ">#=data.IERPH_ERP_NOMBRE#</span>", title: "<span title='" + IERPH_ERP_NOMBRE + "'>" + IERPH_ERP_NOMBRE + "</span>" },

                        { field: "IERPH_NIT", width: 30 },

                        { width: 20, field: "IERPH_ACTIVO", title: "ACTIVO S/N", hidden: $("#HomHomologacionesLabel").html() == 'CONSULTA DE HOMOLOGACIONES' ? true : false },
                        
                        /*
                        { field: "IERPH_ERP_NOMBRE", width: 90 },

                        { field: "", "template": "<input " + css + " title='#=data.IERPH_ERP_CODIGO#' type='button' value='#=data.IERPH_ERP_CODIGO#' onclick='Consultar(\"#=data.IERPH_NIT#\",\"#=data.IERPH_USER#\",\"#=data.IERPH_DATE#\",\"#=data.IERPH_STATUS_DES#\",\"#=data.IERPH_CODIGO#\",\"#=data.IERPH_ERP_CODIGO#\",\"#=data.IERPH_ACTIVO#\",\"#=data.IERPH_NOMBRE#\",\"#=data.IERPH_ERP_NOMBRE#\")'  />", title: "<span title='CODIGO ERP' >" + "ERP" + "</span>", width: 30 },
                        { field: "", "template": "<input " + css + " title='#=data.IERPH_CODIGO# #=data.IERPH_NOMBRE#' type='button' value='#=data.IERPH_CODIGO2#' onclick='Consultar(\"\",\"\",\"\",\"\",\"#=data.IERPH_CODIGO#\",\"\",\"\",\"#=data.IERPH_NOMBRE#\",\"\")'  #=data.IERPH_CODIGO == '' ? 'style=display:none' : ''#  />", title: "<span title='CODIGO HOMOLOGADO CARGO SYSTEM' >" + "CS" + "</span>", width: 30 },                      
                        { field: "IERPH_ERP_NOMBRE", "template": "<span " + css + ">#=data.IERPH_ERP_NOMBRE#</span>", title: "<span title='" + IERPH_ERP_NOMBRE + "'>" + IERPH_ERP_NOMBRE + "</span>", width: 90 },

                        //{ field: "", "template": $("#user_rol").val() == "0" ? '' : '<span style="white-space: nowrap">' + (IERPHC_ID == "321" ? '<input type="button" disabled value="D">' : '<input title=#=data.IERPH_STATUS!="1"?data.IERPH_STATUS_DES:"DES-ASOCIAR"#  type="button" value=#=data.IERPH_STATUS!="1"?data.IERPH_STATUS_DES:"D"# id="EXBT#=data.IERPH_ERP_CODIGO#" #=data.IERPH_ID == "0" || data.IERPH_STATUS != "1" || data.IERPH_ACTIVO != "S" ? "disabled=true" : ""#  onclick="Catalogos_Calls.Desasociar(\'EX\',\'#=data.IERPH_ERP_CODIGO#\')"  />') + '</span>', title: "<span title='DES-ASOCIAR' >" + "D" + "</span>", width: 20 },

                        { width: 15, "template": IERPHC_ID == "321" ? '' : '<button id="EXBT#=data.IERPH_ERP_CODIGO#" #=data.IERPH_ID == "0" || data.IERPH_STATUS != "1" || data.IERPH_ACTIVO != "S" ? "disabled=true" : ""# onclick="Catalogos_Calls.Desasociar(\'EX\',\'#=data.IERPH_ERP_CODIGO#\')" title=#=data.IERPH_STATUS != "1" ? data.IERPH_STATUS_DES : "DES-ASOCIAR"#><span class="k-icon k-i-unlink-vertical colored-red" size="large"></span></button>' },
                        */
                        
                        
                    ], 
                });

            },
            error: function (e) {

                swal({
                    icon: "error",
                    text: e.responseText,
                    button: "Aceptar"
                })

           
            }

        }).done(function (result) {


        }).always(function () {
            //window.kendo.ui.progress($("#gridHomHomologaciones"), false);
            window.kendo.ui.progress($("#gridHomHomologacionesERP"), false);
        });


    },


    HomHomologacionAsociar: function (data, CSrow, EXrow) {


        if (data.IERPHC_ID == '162') {

            if (data.IERPH_TIPO_MONEDA == '') {

                swal({
                    icon: "warning",
                    text: "Seleccione Tipo de Moneda por favor",
                    button: "Aceptar"
                })

                return false;
            }

        }

        swal({
            icon: "warning",
            text: "CS : " + data.CDID + " - ERP : " + data.EXID + "  Confirmar Asociar registros ?",
            buttons: {
                cancel: "Cancelar",
                confirm: { text: "Aceptar", confirmButtonColor: '#044677' }
            },
        })
            .then((willDelete) => {

                if (willDelete) {

                    $.ajax({
                        type: "Get",
                        data: data,
                        url: "/api/Catalogos_Calls_API/SetHomHomologacionAsociar",
                        dataType: "json",
                        contentType: "application/x-www-form-urlencoded",
                        culture: "es-ES",

                        beforeSend: function () {
                            $("#TbLoader").val('1');
                            window.kendo.ui.progress($("#gridHomHomologaciones"), true);
                            window.kendo.ui.progress($("#gridHomHomologacionesERP"), true);
                        },

                        success: function (result) {

                            if (result[0] == "OK") {

                                $("#CS" + data.CDID).val(result[1]);
                                $("#CS" + data.CDID).prop("disabled", true);
                                $("#CSBT" + data.CDID).prop("disabled", false);
                                $(CSrow).find("td:eq(2)").text(data.EXID);

                                var btnDesasociar = "<button title='DES-ASOCIAR : " + data.EXID + " " + data.EXIDStr + "' id='CSBT" + data.CDID + "' onclick=Catalogos_Calls.Desasociar('CS','" + data.CDID + "')><span class='k-icon k-i-unlink-vertical colored-red' size='large'></span> " + data.EXID + "</button >";
                                $(CSrow).find("td:eq(5)").html(btnDesasociar);

                                //console.log(data);


                                $(EXrow).find("td:eq(5)").text('');

                                //if (data.IERPHC_ID != "321") 
                                {
                                    $("#EX" + data.EXID).val(result[1]);
                                    $("#EX" + data.EXID).prop("disabled", true);
                                    $("#EXBT" + data.EXID).prop("disabled", false);
                                    $(EXrow).find("td:eq(2)").text(data.CDID);
                                    
                                    btnDesasociar = "<button title='DES-ASOCIAR : " + data.CDID + " " + data.CDIDStr + "' id='EXBT" + data.EXID + "' onclick=Catalogos_Calls.Desasociar('EX','" + data.EXID + "')><span class='k-icon k-i-unlink-vertical colored-red' size='large'></span> " + data.CDID + "</button >";
                                    $(EXrow).find("td:eq(5)").html(btnDesasociar);
                                }

                                $("#IERPH_TIPO_MONEDA").val("");

                                swal({
                                    icon: "success",
                                    text: "ASOCIACION CORRECTA", 
                                    button: "Aceptar"
                                })

                            } else {

                                swal({
                                    icon: "warning",
                                    text: result[0],
                                    button: "Aceptar"
                                })
                            }

                        },
                        error: function (e) {
                            swal({
                                icon: "error",
                                text: e.responseText,
                                button: "Aceptar"
                            })
                        }

                    }).done(function (result) {



                        return result;
                    }).always(function () {
                        $("#TbLoader").val('');
                        window.kendo.ui.progress($("#gridHomHomologaciones"), false);
                        window.kendo.ui.progress($("#gridHomHomologacionesERP"), false);
                    });


                } else {

                    //carga la tabla.                   
                    //Catalogos_Calls.HomCatalogosGrid();
                }
            });

        return false;
    },


    HomHomologacionDesasociar: function (data, row) {

        swal({

            icon: "warning",
            text: "CS : " + data.CDID + " - ERP : " + data.EXID + " Confirmar Des-asociar registros ?",

            buttons: {
                cancel: "Cancelar",
                confirm: { text: "Aceptar", confirmButtonColor: '#044677' }
            },

        })
            .then((willDelete) => {

                if (willDelete) {

                    $.ajax({
                        type: "Get",
                        data: data,
                        url: "/api/Catalogos_Calls_API/SetHomHomologacionAsociar",
                        dataType: "json",
                        contentType: "application/x-www-form-urlencoded",
                        culture: "es-ES",

                        beforeSend: function () {
                            $("#TbLoader").val('1');
                            window.kendo.ui.progress($("#gridHomHomologaciones"), true);
                            window.kendo.ui.progress($("#gridHomHomologacionesERP"), true);
                        },

                        success: function (result) {

                            if (result[0] == "OK") {

                                var check = $("#CS" + result[2]);
                                $(check).prop("checked", false);
                                $(check).prop("disabled", false);
                                $(check).val("0");
                                $("#CSBT" + result[2]).prop("disabled", true);


                                var check2 = $("#EX" + result[3]);
                                $(check2).prop("checked", false);
                                $(check2).prop("disabled", false);
                                $(check2).val("0");
                                $("#EXBT" + result[3]).prop("disabled", true); 


                                $(row).find("td:eq(2)").text('');
                                $(row).find("td:eq(5)").html(btnAsociar);

                                var row2;

                                if ($(row).find("td:eq(1)").text() == result[2]) {
                                    row2 = $(check2).closest("tr");
                                }

                                if ($(row).find("td:eq(1)").text() == result[3]) {
                                    row2 = $(check).closest("tr");
                                }

                                $(row2).find("td:eq(2)").text('');
                                $(row2).find("td:eq(5)").html(btnAsociar);

                                $("#IERPH_TIPO_MONEDA").val("");


                                swal({
                                    icon: "success",
                                    text: "DES-ASOCIACION CORRECTA",
                                    button: "Aceptar"
                                })



                            } else {

                                swal({
                                    icon: "warning",
                                    text: result[0],
                                    button: "Aceptar"
                                })
                            }

                        },
                        error: function (e) {
                            swal({
                                icon: "error",
                                text: e.responseText,
                                button: "Aceptar"
                            })
                        }

                    }).done(function (result) {
                        return result;
                    }).always(function () {
                        $("#TbLoader").val('');
                        window.kendo.ui.progress($("#gridHomHomologaciones"), false);
                        window.kendo.ui.progress($("#gridHomHomologacionesERP"), false);
                    });

                } else {

                    //carga la tabla.                   
                    //Catalogos_Calls.HomCatalogosGrid();
                }
            });
    },

};

$(document).ready(function () {
    Catalogos_Calls.init();
});

