
function makeBox(m, className)
{
  if (m==="")
    return;
  var d = document.createElement("div");
  d.classList.add(className);
  d.innerHTML = m;
  var messageContainerDiv = document.getElementById("messageContainerDiv");
  if (messageContainerDiv == null)
  {
    messageContainerDiv = document.createElement("div");
    messageContainerDiv.id = "messageContainerDiv";
    d.classList.add("messageContainerDiv");
    document.body.appendChild(messageContainerDiv);
  }
  messageContainerDiv.appendChild(d);

  setTimeout(function() {

    d.style.opacity = '0';
    setTimeout(function() {d.remove();}, 1000);
  }, 4000);
}

function messageSuccess(m)
{
  makeBox(m, "messageSuccess");
}

function messageWarning(m)
{
  makeBox(m, "messageWarning");
}

function messageError(m)
{
  makeBox(m, "messageError");
}

function messageInfo(m)
{
  makeBox(m, "messageInfo");
}

