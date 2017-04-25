function myMessage(sMsg)
{
    var dv;
    dv = document.getElementById('dvMessage');
//    alert('Adesh');
////    if (dv ==null)
////    {  
//       try{
//           dv = document.createElement('div');
//           dv.id = 'dvMessage';
//           dv.style.display = 'block';
//           dv.className = 'xMessage';
//           dv.innerHTML = sMsg;
//           document.form[0].appendChild(dv);
//           setTimeout (removeMessage,5000);
//       }
//       catch(ex){
//        alert(sMsg);        
//       }
//    //}
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

//function myMessage(sMsg)
//{
// var dv;
//    dv = document.getElementById('MessageDiv');
//    alert(dv);
//    if (dv ==null)
//    {  
//       try{
//          // dv = document.createElement('div');
//           //dv.id = 'dvAlertMessage';
//           dv.style.display = 'block';
//           dv.className = 'xMessage';
//           dv.innerHTML = sMsg;
//            alert(sMsg);       
//           //document.body.appendChild(dv);
//           setTimeout (removeMessage,5000);
//       }
//       catch(ex){
//        alert(sMsg);        
//       }
//    }
//    dv.innerHTML = sMsg;

//}

//function removeMessage()
//{
//    try{
//    document.getElementById('MessageDiv').style.display='none';
//    }
//    catch(ex)
//    {}
//}