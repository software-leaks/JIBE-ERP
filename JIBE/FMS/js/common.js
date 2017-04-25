function myMessage(sMsg)
{
    var dv;
    dv = document.getElementById('dvMessage');
    dv.innerHTML = sMsg;
}

function removeMessage()
{
    try{
    document.getElementById('dvAlertMessage').style.display='none';
    }
    catch(ex)
    {}
}

