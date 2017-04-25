function ShowImageBox() {

    document.getElementById('dvUploadimage').style.display = 'block';
    

}


CKEDITOR.plugins.add('insertimage',
{
    init: function (editor) {
        var pluginName = 'insertimage';
        var mypath = this.path;
        editor.ui.addButton('InsertImage',
            {
                label: 'insert image',
                click: ShowImageBox,
                icon: mypath + 'images/insert_image.png'

            });
    }
});


