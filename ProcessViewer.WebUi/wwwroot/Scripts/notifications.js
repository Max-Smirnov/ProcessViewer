var socket;
var notificationsUrl = 'ws://localhost:33077/ws';
var apiUrl = 'http://localhost:4356/api/processes';
var output;
var text = 'test echo';

function log(s) {
    console.log(s);
}

function doConnect() {
    socket = new WebSocket(notificationsUrl);
    socket.onopen = function(e) {
        log('opened ' + notificationsUrl);
        doSend();
    };
    socket.onclose = function (e) {
        log('closed');
        setTimeout(doConnect, 5000);
    };
    socket.onmessage = function (e) {
        log('Received: ' + e.data);
        showNotification(e.data);
    };
    socket.onerror = function(e) {
         log('Error: ' + e.data);
    };
    window.onbeforeunload = function () {
        socket.close();
    };
}

function showNotification(message) {
    var p = document.createElement('p');
    p.attributes.className = 'notification';
    p.innerHTML = new Date() + ': ' + message;
    output.appendChild(p);
}

function doSend() {
    log('Sending: ' + text);
    socket.send(text);
}

function onInit() {
    output = document.getElementsByClassName('notifications-container')[0];
    doConnect();
}

window.onload = function () {
    setTimeout(loadProcesses, 1);
    onInit();
};

function loadProcesses() {
    var jProcs = $('div.processes');

    $.ajax({
        url: apiUrl,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            if (!data.length) {
                jProcs.find('.message').text('Empty data').show();
                setTimeout(loadProcesses, 10000);
            }
            jProcs.find('.message').hide();
            jProcs.find('#processesTable').remove();
            var jTable = $('<table id="processesTable" class="table table-responsive table-hover table-striped table-bordered"></table>');
            var jThead = $('<thead></thead>');
            Object.keys(data[0]).forEach(function(key) {
                jThead.append('<th>' + key + '</th>');
            });
            jThead.appendTo(jTable);
            var jBody = $('<tbody></tbody>');
            for (var i in data) {
                if (!data.hasOwnProperty(i)) continue;
                var jRow = $('<tr></tr>');
                var dataItem = data[i];
                for (var j in dataItem) {
                    if (!dataItem.hasOwnProperty(j)) continue;
                    jRow.append('<td>' + dataItem[j] + '</td>');
                }
                jBody.append(jRow);
            }
            jBody.appendTo(jTable);
            jTable.appendTo(jProcs);
            setTimeout(loadProcesses, 10000);
        },
        error: function (error) {
            jProcs.find('.message').text('Error: '+ JSON.stringify(error)).show();
            setTimeout(loadProcesses, 10000);
        }
    });
};
