
var Menu = {
    init: function () {

        Menu.PageEvent();
        Menu.PageReset();
        $("#inicioM").show();
    },

PageEvent: function () {
    Menu.PageEventInicio();
    Menu.PageEventHomTipoDoc();
    Menu.PageEventHomCuentaC();
    Menu.PageEventayuda();
    //  Menu.PageSaldosDiarios();
    Menu.PageAuditoria();
    // Menu.PageCXC();
    Menu.PageIngresos_cxc();

    //// 2021-04-14 catalogos / paises / empresas
    Menu.PageEventHomCatalogos();
    Menu.PageEventHomPaises();
    Menu.PageEventHomEmpresas();
    Menu.PageEventHomRubros();
    Menu.PageEventHomMonedas();
    Menu.PageEventHomCategorias();
    Menu.PageEventHomUsuarios();

    Menu.PageEventHomQuerys();
    Menu.PageEventHomFiltros();

    Menu.PageEventHomCentroCosto();

    Menu.PageEventHomCatalogosEmpresa();

    Menu.PageEventHomCatalogosModulo();
    Menu.PageEventHomCatalogosSubmit();

    //Menu.PageEventHomCatalogosCate1();
    //Menu.PageEventHomCatalogosCate2();

    Menu.PageEventHomCatalogosClean();
    Menu.PageEventHomCatalogosDelete();




    //// 2021-04-27 homologaciones

    Menu.PageEventHomHomologaciones();  

    Menu.PageEventHomHomologacionesModulo();
    Menu.PageEventHomHomologacionesSubmit();
    Menu.PageEventHomHomologacionesAsociar();


    ///////////////////// Consulta de homologaciones
    Menu.PageEventHomHomologaciones2(); 

 },
    PageReset: function () {     


        RolUsers();

        $("#TbBuscar").val('BUSCAR');

        $("#inicioM").hide();
        $("#HomTipoDocM").hide();
        $("#HomCuentasContableM").hide();
        $("#SaldosDiariosM").hide();
        $("#CXCM").hide();
        $("#IngresosCXCM").hide();
        $("#AuditoriaM").hide();

        //// 2021-04-14 catalogos nueva para SC

     
        $("#HomCatalogosMenu").hide();

   
        $("#TbExcel").hide();



             
        $("#gridHomCatalogosAlls").html("");


        $("#HomCatalogosLabel").html("");
        $("#IERP_TITULO_TITLE").hide();
        $("#IERP_TITULO_COL").hide();
        $("#IERP_TITULO_HTML").hide();
        $("#IERP_CODIGO_TITLE").html("");
        $("#IERP_ERP_CODIGO_TITLE").html("");

        $("#IERP_CODIGO_HTML").html('<input id="IERP_CODIGO" type="text" size="25" maxlength="50" required autocomplete="on" class="HeaderFilters" />');
        $("#IERP_ERP_CODIGO_HTML").html('<input id="IERP_ERP_CODIGO" type="text" size="25" maxlength="50" required autocomplete="on" class="HeaderFilters" />');

        $("#IERP_SQL_HTML").html('');        

        $("#IERP_TITULO_HTML").html('<input id="IERP_TITULO" type="text" size="25" maxlength="60" required autocomplete="on" class="HeaderFilters" />');

        CleanCatalogos();




        //// 2021-04-27 homologaciones


        $("#HomHomologacionesMenu").hide();

        CleanHomologaciones();

        document.getElementById('IERPH_NOMBRE').required = true;
 

        $("#TbLoader").val(''); 

        $("#gridHomHomologaciones").html('<BR><BR><BR><BR><BR><BR><BR><BR>');  
        $("#gridHomHomologacionesERP").html('<BR><BR><BR><BR><BR><BR><BR><BR>');  

        
        $("#IERPH_TIPO_MONEDA_HTML").hide(); //2021-06-08


        $("#IERPE_ID_TITLE").hide();
        $("#IERPE_ID_COL").hide();
        $("#IERPE_ID_HTML").hide();
        document.getElementById('IERPE_ID').required = false;


        $("#TbAsociar").hide();
        $("#ayudaM").hide();     


        $("#gridHomHomologacionesMsg").html('');

        $("#gridHomHomologacionesERPMsg").html('');



    },

 PageEventInicio: function () {
           $("#inicio").click(function () {
             //debugger;
            Menu.PageReset();
            $("#inicioM").show();
         });
    },
 PageEventHomTipoDoc: function () {
           $("#HomTipoDoc").click(function () {
           Menu.PageReset();
          $("#HomTipoDocM").show();
          Acount_Tipo_DOC.HomTipeDocGrid();
         });

    },
 PageEventHomCuentaC: function () {
           $("#HomCuentaC").click(function () {
            Menu.PageReset();
               $("#HomCuentasContableM").show();
               Acount_Cuentas.HomCuentasGrid();
         });
    },
 //PageSaldosDiarios: function () {
         //  $("#SaldosDiarios").click(function () {
          //  Menu.PageReset();
          //     $("#SaldosDiariosM").show();
           //    Int_Saldos.Int_saldosALL();
        // });
   // },
 PageAuditoria: function () {
           $("#Auditoria").click(function () {
            Menu.PageReset();
               $("#AuditoriaM").show();
               Int_Auditoria.Int_AuditoriaALL();
         });
    },




 //// 2021-04-14 catalogos / paises / empresas 


    PageEventHomCatalogos: function () {
        $("#HomCatalogos").click(function () {
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
            document.getElementById('IERP_TITULO').required = true;
            $("#HomCatalogosLabel").html("ADMIN CATALOGOS");
            $("#IERP_TITULO_TITLE").show();
            $("#IERP_TITULO_COL").show();
            $("#IERP_TITULO_HTML").show();
            $("#IERP_CODIGO_TITLE").html("Codigo del Catalogo OPERATIVO");
            $("#IERP_ERP_CODIGO_TITLE").html("Codigo del Catalogo ERP");
            $("#HomCatalogosMenu").show();

            $("#IERP_CODIGO_HTML").html('');
            $("#IERP_ERP_CODIGO_HTML").html('');

            Catalogos_Calls.GetModuleSelect();
        });
    },

    
    PageEventHomPaises: function () {
        $("#HomPaises").click(function () {           
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
            document.getElementById('IERP_TITULO').required = false; 
            $("#HomCatalogosLabel").html("ADMIN PAISES");

            $("#IERP_TITULO_TITLE").hide();
            $("#IERP_TITULO_COL").hide();
            $("#IERP_TITULO_HTML").hide();

            $("#IERPE_ID_TITLE").show();
            $("#IERPE_ID_COL").show();
            $("#IERPE_ID_HTML").show();
            document.getElementById('IERPE_ID').required = true;



            $("#IERP_CODIGO_TITLE").html("Codigo de Pais OPERATIVO");
            $("#IERP_ERP_CODIGO_TITLE").html("Codigo de Pais ERP");

            $("#IERP_CODIGO_HTML").html('');
            $("#IERP_ERP_CODIGO_HTML").html('');

            $("#HomCatalogosMenu").show();
            Catalogos_Calls.GetModuleSelect();          
        });
    },



    PageEventHomUsuarios: function () {
        $("#HomUsuarios").click(function () {
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
                document.getElementById('IERP_TITULO').required = false;
            $("#HomCatalogosLabel").html("ADMIN USUARIOS");

            $("#IERP_TITULO_TITLE").hide();
            $("#IERP_TITULO_COL").hide();
            $("#IERP_TITULO_HTML").hide();

            $("#IERPE_ID_TITLE").show();
            $("#IERPE_ID_COL").show();
            $("#IERPE_ID_HTML").show();
            document.getElementById('IERPE_ID').required = true;

            $("#IERP_CODIGO_TITLE").html("Usuario");
            $("#IERP_ERP_CODIGO_TITLE").html("Roll");

            $("#IERP_CODIGO_HTML").html('');
            $("#IERP_ERP_CODIGO_HTML").html('');

            $("#HomCatalogosMenu").show();
            Catalogos_Calls.GetModuleSelect();
        });
    },


    PageEventHomCentroCosto: function () {
        $("#HomCentroCosto").click(function () {
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
                document.getElementById('IERP_TITULO').required = false;
            $("#HomCatalogosLabel").html("ADMIN CENTRO COSTO");

            $("#IERP_TITULO_TITLE").hide();
            $("#IERP_TITULO_COL").hide();
            $("#IERP_TITULO_HTML").hide();

            $("#IERPE_ID_TITLE").show();
            $("#IERPE_ID_COL").show();
            $("#IERPE_ID_HTML").show();
            document.getElementById('IERPE_ID').required = true;

            $("#IERP_CODIGO_TITLE").html("Servicio CS");
            $("#IERP_ERP_CODIGO_TITLE").html("Centro Costo ERP");

            $("#IERP_CODIGO_HTML").html('');
            //$("#IERP_ERP_CODIGO_HTML").html('');

            $("#HomCatalogosMenu").show();
            Catalogos_Calls.GetModuleSelect();
        });
    },

    PageEventHomEmpresas: function () {
        $("#HomEmpresas").click(function () {
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
            document.getElementById('IERP_TITULO').required = false; 
            $("#HomCatalogosLabel").html("ADMIN EMPRESAS");
            $("#IERP_TITULO_TITLE").hide();
            $("#IERP_TITULO_COL").hide();
            $("#IERP_TITULO_HTML").hide();
            $("#IERP_CODIGO_TITLE").html("Codigo de Empresa OPERATIVO");
            $("#IERP_ERP_CODIGO_TITLE").html("Codigo de Empresa ERP");

            $("#IERP_CODIGO_HTML").html('');
            $("#IERP_ERP_CODIGO_HTML").html('');

            $("#HomCatalogosMenu").show();
            Catalogos_Calls.GetModuleSelect();
        });
    },


    PageEventHomRubros: function () {
        $("#HomRubros").click(function () {
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
                document.getElementById('IERP_TITULO').required = false;
            $("#HomCatalogosLabel").html("ADMIN RUBROS");
            $("#IERP_TITULO_TITLE").hide();
            $("#IERP_TITULO_COL").hide();
            $("#IERP_TITULO_HTML").hide();
            $("#IERP_CODIGO_TITLE").html("Codigo de Rubro OPERATIVO");
            $("#IERP_ERP_CODIGO_TITLE").html("Codigo de Rubro ERP");
            $("#HomCatalogosMenu").show();
            Catalogos_Calls.GetModuleSelect();
        });
    },


    PageEventHomMonedas: function () {
        $("#HomMonedas").click(function () {
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
            document.getElementById('IERP_TITULO').required = false;
            $("#HomCatalogosLabel").html("ADMIN MONEDAS");
            $("#IERP_TITULO_TITLE").hide();
            $("#IERP_TITULO_COL").hide();
            $("#IERP_TITULO_HTML").hide();
            $("#IERP_CODIGO_TITLE").html("Codigo de Moneda OPERATIVO");
            $("#IERP_ERP_CODIGO_TITLE").html("Codigo de Moneda ERP");
            $("#HomCatalogosMenu").show();
            //Catalogos_Calls.GetModuleSelect();

            Catalogos_Calls.GetTest();
        });
    },



    PageEventHomCategorias: function () {
        $("#HomCategorias").click(function () {
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
            document.getElementById('IERP_TITULO').required = true;
            $("#HomCatalogosLabel").html("ADMIN CATEGORIAS");
            $("#IERP_TITULO_TITLE").show();
            $("#IERP_TITULO_COL").show();
            $("#IERP_TITULO_HTML").show();
            $("#IERP_CODIGO_TITLE").html("Codigo de Categoria");

            $("#IERP_ERP_CODIGO_TITLE").html("Sistema");

            $("#IERP_ERP_CODIGO_HTML").html('<select id="IERP_ERP_CODIGO" required class="HeaderFilters"></select>');

            $("#IERP_TITULO_TITLE").html("Descripcion de Categoria");

            $("#HomCatalogosMenu").show();
            Catalogos_Calls.GetModuleSelect();
        });
    },


    PageEventHomQuerys: function () {
        $("#HomQuerys").click(function () {
            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
            document.getElementById('IERP_TITULO').required = true;
            $("#HomCatalogosLabel").html("ADMIN QUERYS");
            $("#IERP_TITULO_TITLE").show();
            $("#IERP_TITULO_COL").show();
            $("#IERP_TITULO_HTML").show();

            $("#IERP_TITULO_TITLE").html("Clave");
            $("#IERP_CODIGO_TITLE").html("Ordenamiento");
            $("#IERP_ERP_CODIGO_TITLE").html("Sistema");

            $("#IERP_ERP_CODIGO_HTML").html('<select id="IERP_ERP_CODIGO" required class="HeaderFilters"></select>');

            $("#IERP_SQL_HTML").html('<th scope="col" class="k-header" style="vertical-align:top">SQL</th><th colspan="5"><textarea id="IERP_SQL" style="width:100%;" rows="4"></textarea></th>');

            $("#HomCatalogosMenu").show();
            Catalogos_Calls.GetModuleSelect();
        });
    },


    PageEventHomFiltros: function () {
        $("#HomFiltros").click(function () {

            Menu.PageReset();
            if (document.getElementById('IERP_TITULO'))
            document.getElementById('IERP_TITULO').required = true;
            $("#HomCatalogosLabel").html("ADMIN FILTROS");
            $("#IERP_TITULO_TITLE").show();
            $("#IERP_TITULO_COL").show();
            $("#IERP_TITULO_HTML").show();

            $("#IERP_TITULO_TITLE").html("Sistema - Clave - Tabla");
            $("#IERP_CODIGO_TITLE").html("Campo a Filtrar");
            $("#IERP_ERP_CODIGO_TITLE").html("Comparativo");

            $("#IERP_TITULO_HTML").html('<select id=IERP_TITULO required class=HeaderFilters onchange=CategoriasDescripcion();></select><span class="k-header" style="padding:4px;display:block;margin-top:6px;width:100%">Operador Logico</span><select id=IERP_OPERADOR class=HeaderFilters><option value="">Seleccione</option><option value="AND">AND</option><option value="OR">OR</option></select>');

            $("#IERP_CODIGO_HTML").html('<input id="IERP_CODIGO" type="text" size="15" maxlength="50" required autocomplete="on" class="HeaderFilters" /><span class="k-header" style="padding:4px;display:block;margin-top:6px;width:100%">Campo Equivalente Progra</span><input id="IERP_CAMPO_PROGRA" type="text" size="15" maxlength="30" required autocomplete="on" class="HeaderFilters" />');
            $("#IERP_ERP_CODIGO_HTML").html('<input id="IERP_ERP_CODIGO" type="text" size="10" maxlength="10" required autocomplete="on" class="HeaderFilters" /><span class="k-header" style="padding:4px;display:block;margin-top:6px;width:100%">Orden en que se muestra</span><input id="IERP_ORDEN" type="number" size="5" maxlength="5" required class="HeaderFilters" />');

            //$("#IERP_CODIGO_HTML").height('30px');
            //$("#IERP_ERP_CODIGO_HTML").height('30px');


            $("#IERP_CAMPO_PROGRA").show();
            $("#IERP_ORDEN").show();
            $("#IERP_OPERADOR").show();

            $("#HomCatalogosMenu").show();
            Catalogos_Calls.GetModuleSelect();
        });
    },


    PageEventHomCatalogosEmpresa: function () {
        $("#IERPHE_ID").change(function () {

            if ($("#IERPHE_ID").val() == "") {
                CleanHomologaciones();
                $("#IERPHM_ID").val("");
            }

        });
    },


    PageEventHomCatalogosModulo: function () {
        $("#IERPM_ID").change(function () {

            OnChangeModulo();
        });
    },


    PageEventHomCatalogosSubmit: function () {
        $("#Form_IB_CATALOGOS").submit(function () {


            var IERP_CAMPO_PROGRA = document.getElementById('IERP_CAMPO_PROGRA') ? document.getElementById('IERP_CAMPO_PROGRA').value : "";
            var IERP_ORDEN = document.getElementById('IERP_ORDEN') ? document.getElementById('IERP_ORDEN').value : "";


            if ($("#HomCatalogosLabel").html() == "ADMIN EMPRESAS") { //2021-06-29 se usaron estos parametros para las empresas

                IERP_CAMPO_PROGRA = $("#IERP_CODIGO option:selected").text();
                IERP_ORDEN = $("#IERP_ERP_CODIGO option:selected").text();
            }

            var data = {
                'OPE': document.getElementById('IERP_ID') ? (document.getElementById('IERP_ID').value == '' ? 'INS' : 'UPD') : '',
                'IERP_ID': document.getElementById('IERP_ID') ? document.getElementById('IERP_ID').value : '',
                'IERPM_ID': document.getElementById('IERPM_ID') ? document.getElementById('IERPM_ID').value : '',
                'IERPM_DES': $("#IERPM_ID") ? $("#IERPM_ID option:selected").text() : '',
                'IERP_TITULO': document.getElementById('IERP_TITULO') ? document.getElementById('IERP_TITULO').value : '',
                'IERP_CODIGO': document.getElementById('IERP_CODIGO') ? document.getElementById('IERP_CODIGO').value : '',
                'IERP_ERP_CODIGO': document.getElementById('IERP_ERP_CODIGO') ? document.getElementById('IERP_ERP_CODIGO').value : '',
                'IERPS_SEC': document.getElementById('IERPS_SEC') ? document.getElementById('IERPS_SEC').value : '',

                'IERP_OPCION': $("#HomCatalogosLabel") ? $("#HomCatalogosLabel").html() : '',

                'usuario': document.getElementById('user_carrousel') ? document.getElementById('user_carrousel').value : '',

                'IERP_CAMPO_PROGRA': IERP_CAMPO_PROGRA,
                'IERP_ORDEN': IERP_ORDEN,

                'IERP_SQL': document.getElementById('IERP_SQL') ? $('#IERP_SQL').val() : "",
                'IERP_OPERADOR': document.getElementById('IERP_OPERADOR') ? document.getElementById('IERP_OPERADOR').value : "",
                'IERPC_ROL': document.getElementById('user_rol') ? document.getElementById('user_rol').value : "",
                'IERPHE_ID': document.getElementById('IERPE_ID') ? document.getElementById('IERPE_ID').value : "",
            };

            debugger

            if ($("#IERP_CODIGO option:selected"))
                data.IERP_TITULO = $("#IERP_CODIGO option:selected").text();

            Catalogos_Calls.Save_IB_CATALOGOS(data);
          


            return false;
        });
    },


    PageEventHomCatalogosClean: function () {
        $("#TbNuevo").click(function () {

            CleanCatalogos();

            return false;
        });
    },


    PageEventHomCatalogosDelete: function () {
        $("#TbDelete").click(function () {

            if ($("#TbLoader").val() == '1')
                return false;

            if (document.getElementById('IERP_ID').value == "")
                return false;


            swal({
                icon: "warning",
                text: "Confirmar Borrar Registro ?",
                buttons: {
                    cancel: "Cancelar",
                    confirm: { text: "Aceptar", confirmButtonColor: '#044677' }
                },
            })
                .then((willDelete) => {

                    if (willDelete) {


                        var data = {
                            'OPE': 'DEL',
                            'IERP_ID': document.getElementById('IERP_ID') ? document.getElementById('IERP_ID').value : '',
                            'IERPM_ID': document.getElementById('IERPM_ID') ? document.getElementById('IERPM_ID').value : '',
                            'IERPM_DES': $("#IERPM_ID") ? $("#IERPM_ID option:selected").text() : '',
                            'IERP_TITULO': document.getElementById('IERP_TITULO') ? document.getElementById('IERP_TITULO').value : '',
                            'IERP_CODIGO': document.getElementById('IERP_CODIGO') ? document.getElementById('IERP_CODIGO').value : '',
                            'IERP_ERP_CODIGO': document.getElementById('IERP_ERP_CODIGO') ? document.getElementById('IERP_ERP_CODIGO').value : '',
                            'IERPS_SEC': document.getElementById('IERPS_SEC') ? document.getElementById('IERPS_SEC').value : '',
                            'IERP_OPCION': $("#HomCatalogosLabel") ? $("#HomCatalogosLabel").html() : '',
                            'usuario': document.getElementById('user_carrousel') ? document.getElementById('user_carrousel').value : '',
                            'IERP_CAMPO_PROGRA': document.getElementById('IERP_CAMPO_PROGRA') ? document.getElementById('IERP_CAMPO_PROGRA').value : "",
                            'IERP_ORDEN': document.getElementById('IERP_ORDEN') ? document.getElementById('IERP_ORDEN').value : "",
                            'IERP_SQL': document.getElementById('IERP_SQL') ? $('#IERP_SQL').val() : "",
                            'IERP_OPERADOR': document.getElementById('IERP_OPERADOR') ? document.getElementById('IERP_OPERADOR').value : "",
                            'IERPC_ROL': document.getElementById('user_rol') ? document.getElementById('user_rol').value : "",
                            'IERPHE_ID': document.getElementById('IERPE_ID') ? document.getElementById('IERPE_ID').value : "",
                        };


                        Catalogos_Calls.Save_IB_CATALOGOS(data);

                    } else {

                    }
                });



            return false;
        });
    },





    ////////////// 2021-04-27



    PageEventHomHomologaciones: function () {
        $("#HomHomologaciones").click(function () {
            Menu.PageReset();
            $("#HomHomologacionesMenu").show();
            $("#gridHomHomologaciones").show();
            $("#gridHomHomologacionesERP").show();
            $("#IERPHM_CHK").hide();           
            $("#IERPHM_LABEL").hide();
            $("#HomHomologacionesLabel").html('OPERACION DE HOMOLOGACIONES'); 

            //RolUsers();

            Catalogos_Calls.GetModuleSelect();

            document.getElementById('IERPHC_ID').required = true;
            document.getElementById('IERPHP_ID').required = true;
            document.getElementById('IERPHE_ID').required = true;

        });
    },


     
    PageEventHomHomologaciones2: function () {
        $("#HomHomologaciones2").click(function () {
            Menu.PageReset();
            $("#HomHomologacionesMenu").show();
            $("#gridHomHomologaciones").show();
            $("#gridHomHomologacionesERP").show();
            $("#IERPHM_CHK").show();
            $("#IERPHM_LABEL").show();
            $("#HomHomologacionesLabel").html('CONSULTA DE HOMOLOGACIONES');
            $("#TbAsociar").hide();
            document.getElementById('IERPH_NOMBRE').required = false;
            $("#TbBuscar").val('CONSULTAR');

            document.getElementById('IERPHC_ID').required = false;
            document.getElementById('IERPHP_ID').required = false;
            document.getElementById('IERPHE_ID').required = false;

            //RolUsers();

            Catalogos_Calls.GetModuleSelect();
        });
    },


    PageEventHomHomologacionesModulo: function () {
        $("#IERPHM_ID").change(function () {
            //Menu.PageReset();
            // $("#HomCatalogosMenu").show();

            var user_rol = document.getElementById('user_rol').value;

            //alert(user_rol);

            var data = {
                'IERPHM_ID': this.value,
                'IERPH_OPCION': $("#HomHomologacionesLabel").html(),
                'IERPC_ROL': user_rol, //$("#user_rol").val(),
                'IERPHE_ID': $("#IERPHE_ID").val(),
            };

            if (this.value == '') {

                $("#TbAsociar").hide();

                CleanHomologaciones2();

                $("#TbLoader").val('');

                $("#gridHomHomologaciones").html('<BR><BR><BR><BR><BR><BR><BR><BR>');
                $("#gridHomHomologacionesERP").html('<BR><BR><BR><BR><BR><BR><BR><BR>');

                CleanHomologaciones();


            } else {


                if ($("#IERPHE_ID").val() == "") {

                    this.value = '';

                    swal({
                        icon: "info",
                        text: "Debe seleccionar Empresa antes del Modulo",
                        button: "Aceptar"
                    });

                    return false;

                } else {

                    Catalogos_Calls.Get_IB_HOMOLOGACIONES(data);

                }
            }
        });
    },



    PageEventHomHomologacionesSubmit: function () {
        $("#Form_IB_HOMOLOGACIONES").submit(function () {



            var continuar = true;

            var IERPH_NOMBRE = $("#IERPH_NOMBRE").val();
            var IERPH_NIT = $("#IERPH_NIT").val();

            if (IERPH_NOMBRE.trim() == '' && IERPH_NIT.trim() == '') {

                swal({
                    icon: "warning",
                    text: "Puede ser que su consulta sin filtros, demore demasiado para obtener los resultados, desea continuar ?",
                    buttons: {
                        cancel: "Cancelar",
                        confirm: { text: "Aceptar", confirmButtonColor: '#044677' }
                    },
                })
                    .then((willDelete) => {

                        if (willDelete) {

                            Catalogos_Calls.SubmitCombos();


                        } else {

                            return false;

                        }
                    });

            } else {

                Catalogos_Calls.SubmitCombos();

            }



            return false;
        });
    },


    PageEventHomHomologacionesAsociar: function () {
        $("#TbAsociar").click(function () {

            AsociarRegistros(0);

        });
    },


    PageEventayuda: function () {
         $("#ayuda").click(function () {
          Menu.PageReset();
         $("#ayudaM").show();
         });
    },
PageIngresos_cxc: function () {
           $("#IngresosCXC").click(function () {
            Menu.PageReset();
               $("#IngresosCXCM").show();
               Int_Ingresos_CXC.Int_Ingresos_cxcALL();
         });
    }
};

$(document).ready(function () {
    Menu.init();
});

