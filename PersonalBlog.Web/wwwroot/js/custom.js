$(document).ready(function(){
    console.log("logged1");
    LoadckEditor4();
});

function LoadckEditor4(){
    console.log("logged2");
    if(!document.getElementById("ckEditor4"))
        return;

    $("body").append("<script src='/ckeditor/ckeditor.js'></script>");

    CKEDITOR.replace('ckEditor4',
        {
            customConfig: '/ckeditor/config.js'
        });
}