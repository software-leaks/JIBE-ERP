var isloaded = false;

var lastExecutorDetails = null;


function Get_Video_FileName(id) {


    if (lastExecutorDetails != null)
        lastExecutorDetails.abort();

    var service = Sys.Net.WebServiceProxy.invoke('../JIBEWebService.asmx', 'Get_Video_FileName', false, { "id": id }, OnSuccGet_Video_FileName, Onfail, new Array(null));
    lastExecutorDetails = service.get_executor();


}

var itemName = "";
function OnSuccGet_Video_FileName(retval, prm) {
    try {

        $("#ctl00_MainContent_hfFileName").attr('value', retval);
        NodeClick();

    }
    catch (ex)
    { }
}

function Onfail() {
    alert('service')
}


function load_FunctionalTree(id) {







    var vessel_id = 93;
    var Equipment_Type = 1;

    if (isloaded == true) {
        $("#FunctionalTree").jstree("destroy");
        isloaded = true;
    }

    if (vessel_id.toString() != "0") {


        $("#FunctionalTree").jstree({
            'core': {
                'data': {
                    'type': "POST",
                    "async": "true",
                    'contentType': "application/json; charset=utf-8",
                    'url': "../JIBEWebService.asmx/Get_Function_Tree_Videos",
                    'data': "{}",
                    'dataType': 'JSON',
                    'data': function (node) {
                        if (node.id.toString() != '#')
                            return '{"id":' + node.id + '}'
                        else
                            return '{"id":null }'
                    },
                    'success': function (retvel) {
                        isloaded = true;
                        return retvel.d;


                    }

                }
            }
        });


        $("#FunctionalTree").bind('select_node.jstree', function (event, data) {

            var prm = $("#FunctionalTree").jstree('get_selected').toString().split(',');


            $("#ctl00_MainContent_hfPar").attr('value', data.node.parent);
            $("#ctl00_MainContent_hfNode").attr('value', data.node.id);
            $("#ctl00_MainContent_hfText").attr('value', data.node.text);



            itemName = data.node.text;

            if (data.node.parent == "#") {

                $("#ctl00_MainContent_btnAdd").show();
               


            }
            else {
                if (data.node.parent == "1") {

                    $("#ctl00_MainContent_btnAdd").show();
                    

                }
                else {
                    $("#ctl00_MainContent_btnAdd").hide();
                    Get_Video_FileName(data.node.id);
                }
            }














            //    $("#ctl00_MainContent_btnAdd").prop('value', $("#FunctionalTree").jstree('get_selected').toString()));



            if (prm.length > 1) {
                if (prm.length == 4) {

                }
                else if (prm.length == 2) {


                }


                $("#tabs").tabs('option', 'active', 0);
            }


        });

    }





}