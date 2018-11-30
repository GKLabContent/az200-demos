// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    //Add a student
    $("#newStudent").click(function () {
        $('#status').text("Adding student.");
        var payload = '{ "ID": "1", "Name": "' + $('#studentName').val() + '", "RegisteredOn": "01-01-2018" }';
        $.post(
            {
                'url': 'api/School',
                'data': payload,
                'contentType':'application/json'
            }
        ).done(function () { $('#status').text("Added student"); $('#studentName').val(""); }).fail(function () { $('#status').text("Failed to add student."); });
    });
    getStudents();

    //Retrieve the student list every 5 seconds.
    var timerController = setInterval(getStudents, 5000);

    //Function to retrieve student list.
    function getStudents() {
        $('#status').text("Retrieving students.");
        $.getJSON('api/School').done(function (data) {
            var list = "";
            $(data).each(function (index, student) {
                list += "<tr><td>" + student.id + "</td><td>" + student.name + "</td><td>" + student.registeredOn + "</td></tr>";
            });
            $('#studentList').html(list);
            $('#status').text("Retrieved students.");
        }).fail(function () { $('#status').text("Failed to retrieve students"); });
    }
});