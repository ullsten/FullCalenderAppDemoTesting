
document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        // Calendar options...

        //contentHeight: 200,
        timeZone: 'UTC',
        locale: 'sv', // Set the locale to Swedish

        initialView: 'dayGridMonth',
        //initialView: 'multiMonthYear',
        //multiMonthMaxColumns: 2, // force a single column

        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay',
        },
        buttonText: {
            prev: 'Föregående',
            next: 'Nästa',
            today: 'Idag',
            month: 'Månad',
            week: 'Vecka',
            day: 'Dag',
        },

        //set default color for event (bordercolor ?)
        //eventColor: 'green',
        //eventTextColor: 'black',
        //eventBorderColor: 'yellow',
        eventDisplay: 'auto',
        displayEventTime: false,
        dayMaxEvents: true, // when too many events in a day, show the popover
        editable: true, // Allow events to be dragged and resized
        selectable: true, // Enable date selection

        events: getEvents(), // get events for database to calendar
    });

    // Function to get events from the database
    function getEvents() {
        return {
            url: 'Home/GetEvents',
            failure: function () {
                alert('Failed to retrieve events. Please try again later.');
            },
            eventDataTransform: function (eventData) {
                // Modify the event data here
                eventData.backgroundColor = eventData.themeColor; // set backgroundColor for event from the database
                return eventData;
            }
        };
    }

    // Function to save or update an event
    function saveEvent(eventId, subject, description, isFullDay, themeColor, start, end) {
        $.ajax({
            url: 'Home/SaveEvent',
            type: 'POST',
            data: {
                eventId: eventId,
                Subject: subject,
                Description: description,
                IsFullDay: isFullDay,
                ThemeColor: themeColor,
                Start: start.toISOString(),
                End: end.toISOString(),
            },
            success: function (response) {
                $('#modalTitle').text('Success');
                $('#modalBody').text('Event dates successfully updated.');
                $('#myModal').modal('show');
            },
            error: function () {
                console.log('AJAX Error');
                $('#modalTitle').text('Error');
                $('#modalBody').text('Failed to update event. Please try again later.');
                $('#myModal').modal('show');
            }
        });
    }

    // Function to delete an event
    function deleteEvent(eventId) {
        $.ajax({
            type: 'POST',
            url: 'Home/DeleteEvent',
            data: {
                eventId: eventId
            },
            success: function (data) {
                if (data.status) {
                    $('#eventDetailModal').modal('hide');
                }
                calendar.refetchEvents();
                alert('Event successfully deleted.');
            },
            error: function () {
                alert('Failed to delete event. Please try again later.');
            }
        });
    }

    // Event Handlers...
    calendar.on('select', function (info) {
        var start = moment(info.start).format('YYYY-MM-DDTHH:mm:ss');
        var end = moment(info.end).format('YYYY-MM-DDTHH:mm:ss');
        document.getElementById('start').value = start;
        document.getElementById('end').value = end;
        $('#CreateEventModal').modal('show');
    });

    calendar.on('eventDrop', function (dropInfo) {
        var eventId = dropInfo.event.extendedProps.eventId;
        var newStart = dropInfo.event.start;
        var newEnd = dropInfo.event.end;
        var subject = dropInfo.event.title;
        var description = dropInfo.event.extendedProps.description;
        var isFullDay = dropInfo.event.extendedProps.isFullDay;
        var themeColor = dropInfo.event.backgroundColor;

        if (typeof eventId === 'undefined' || eventId === null) {
            alert('Event ID is undefined or null.');
            return;
        } else if (!confirm("Are you sure about this change?")) {
            dropInfo.revert();
        } else {
            saveEvent(eventId, subject, description, isFullDay, themeColor, newStart, newEnd);
        }
    });

    calendar.on('eventClick', function (info) {
        var selectedEvent = info.event;
        var eventDetails = document.createElement('div');
        eventDetails.innerHTML = `
    <p><strong>Subject:</strong> ${selectedEvent.title}</p>
    <p><strong>Description:</strong> ${selectedEvent.extendedProps.description}</p>
    <p><strong>Start:</strong> ${moment(selectedEvent.start).format('YYYY-MM-DD:mm:ss')}</p>
    <p><strong>End:</strong> ${selectedEvent.extendedProps.isFullDay ? moment(selectedEvent.start).format('YYYY-MM-DD:mm:ss') : moment(selectedEvent.end).format('YYYY-MM-DD:mm:ss')}</p>
    `;

        var eventModalBody = document.getElementById('eventModalBody');
        eventModalBody.innerHTML = '';
        eventModalBody.appendChild(eventDetails);

        $('#eventDetailModal').modal('show');

        $('#editButton').click(function () {
            var eventId = selectedEvent.extendedProps.eventId;
            var eventTitle = selectedEvent.title;
            var eventDescription = selectedEvent.extendedProps.description;
            var eventStart = moment(selectedEvent.start).format('YYYY-MM-DDTHH:mm:ss');
            var eventEnd = moment(selectedEvent.end).format('YYYY-MM-DDTHH:mm:ss');
            var eventColor = selectedEvent.extendedProps.themeColor;
            var isFullDay = selectedEvent.extendedProps.isFullDay;

            $('#editEventId').val(eventId);
            $('#editSubject').val(eventTitle);
            $('#editDescription').val(eventDescription);
            $('#editStart').val(eventStart);
            $('#editEnd').val(eventEnd);
            $('#editThemeColor').val(eventColor);
            $('#editIsFullDay').prop('checked', isFullDay);

            if (isFullDay) {
                $('#editEndDateForm').hide();
            } else {
                $('#editEndDateForm').show();
                $('#editEnd').val(eventEnd);
            }

            $('#editEventModal').modal('show');
            $('#eventDetailModal').modal('hide');
        });

        $('#deleteEvent').click(function () {
            if (selectedEvent != null && confirm('Are you sure you want to delete this event?')) {
                deleteEvent(selectedEvent.extendedProps.eventId);
            }
        });
    });

    // Checkbox Change Event Handler...
    $('#isFullDay, #editIsFullDay').change(function () {
        if ($(this).is(':checked')) {
            $('#endDateForm, #editEndDateForm').hide();
            $('#isFullDay, #editIsFullDay').val(true);
            var startDateValue = $('#start').val();
            $('#end, #editEnd').val(startDateValue);
        } else {
            $('#endDateForm, #editEndDateForm').show();
            $('#isFullDay, #editIsFullDay').val(false);
        }
    });

    // Render the calendar
    calendar.render();
});
