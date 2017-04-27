/* ----------------------------------------------------------------------------- 

  jQuery DateTimePicker - Responsive flat design jQuery DateTime Picker plugin for Web & Mobile
  Version 0.1.4
  Copyright (c)2015 Curious Solutions Pvt Ltd and Neha Kadam
  http://curioussolutions.github.io/DateTimePicker
  https://github.com/CuriousSolutions/DateTimePicker

 ----------------------------------------------------------------------------- */

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (elt /*, from*/) {
        var len = this.length;

        var from = Number(arguments[1]) || 0;
        from = (from < 0) ? Math.ceil(from) : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++) {
            if (from in this &&
				this[from] === elt)
                return from;
        }
        return -1;
    };
}

jQuery.fn.fadeIn = function () {
    this.show();
}

jQuery.fn.fadeOut = function () {
    this.hide();
}

$(document).ready(function () {
    var newdate = null;
    var getmin = $('.mindate').val();
    if (getmin != null)
    {
        var dateObj = new Date();
        var day = dateObj.getUTCDate();
        var month = dateObj.getUTCMonth();
        var year = dateObj.getUTCFullYear();
        newdate = day + "-" + (month + 1) + "-" + year;
    }
    $("#dtBox").DateTimePicker(
        {
            minDate: newdate
        });
});

function speNightsChange(val, arrivalDate, departureDate, departureID) {
    //get our initial date
    //var arrivalDate = document.getElementById('dteArrival').value
    //var departureDate = document.getElementById('dteDeparture').value
    AddDate(arrivalDate, departureDate, val, departureID)
}

function dteArrivalChange(arrivalDate, departureDate, departureID) {
    //var departureDate = document.getElementById('dteDeparture').value
    ChangeDate(arrivalDate, departureDate, 2, departureID)
}

function dteDepartureChange(departureDate, arrivalDate, departureID) {
    //var arrivalDate = document.getElementById('dteArrival').value
    ChangeDate(arrivalDate, departureDate, 2, departureID)
}

function ChangeDate(arrivalDate, departureDate, type, departureID) {
    var dateArr = arrivalDate.split('-')
    var dateObj = new Date(dateArr[2], (dateArr[1] - 1), dateArr[0])
    var dateArr2 = departureDate.split('-')
    var dateObj2 = new Date(dateArr2[2], (dateArr2[1] - 1), dateArr2[0])
    if (type == 1) {
        if (dateObj >= dateObj2) {
            AddDate(arrivalDate, departureDate, 1)
        }
        else {
            CaculatorDate(arrivalDate, departureDate)
        }
    }
    else if (type == 2) {
        if (dateObj > dateObj2) {
            document.getElementById(departureID).value = arrivalDate;
        }
    }
}

function AddDate(arrivalDate, departureDate, value, departureID) {
    if (arrivalDate.length > 0 && arrivalDate.indexOf('-') != -1) {
        var dateArr = arrivalDate.split('-')
        if (dateArr.length == 3) {
            //create a new date object with the initial date
            var dateObj = new Date(dateArr[2], (dateArr[1] - 1), dateArr[0])
            //call our new prototype method to add days to a date object
            var newDate = dateObj.addDays(value)

            var day = newDate.getDate();
            day = day < 10 ? '0' + day : day;
            var month = newDate.getMonth() + 1;
            month = month < 10 ? '0' + month : month;
            var year = newDate.getFullYear();
            var newdate2 = day + "-" + month + "-" + year;
            //our newdate object will contain the result from our operation.
            document.getElementById(departureID).value = newdate2;
            CaculatorDate(arrivalDate, newdate2)
        } else {
            alert('The date entered is invalid')
        }
    }
}

function CaculatorDate(arrivalDate, departureDate) {

    //Get 1 day in milliseconds
    var one_day = 1000 * 60 * 60 * 24;

    var dateArr = arrivalDate.split('-')
    var dateObj = new Date(dateArr[2], (dateArr[1] - 1), dateArr[0])
    var dateArr2 = departureDate.split('-')
    var dateObj2 = new Date(dateArr2[2], (dateArr2[1] - 1), dateArr2[0])

    // Convert both dates to milliseconds
    var date1_ms = dateObj.getTime();
    var date2_ms = dateObj2.getTime();

    // Calculate the difference in milliseconds
    var difference_ms = date2_ms - date1_ms;
    // Convert back to days and return
    var realDate = Math.round(difference_ms / one_day);

    if (document.getElementById('speNights') != null)
        document.getElementById('speNights').value = realDate;
}

Date.prototype.addDays = function (s) {

    var targetDays = parseInt(s)
    var thisYear = parseInt(this.getFullYear())
    var thisDays = parseInt(this.getDate())
    var thisMonth = parseInt(this.getMonth() + 1)

    var currDays = thisDays;
    var currMonth = thisMonth;
    var currYear = thisYear;

    var monthArr;

    var nonleap = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    // leap year
    var leap = [31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    if ((thisYear % 4) == 0) {
        if ((thisYear % 100) == 0 && (thisYear % 400) != 0) { monthArr = nonleap; }
        else { monthArr = leap; }
    }
    else { monthArr = nonleap; }

    var daysCounter = 0;
    var numDays = 0;
    var monthDays = 0;

    if (targetDays < 0) {

        while (daysCounter < (targetDays * -1)) {

            if (daysCounter == 0) {
                if ((targetDays * -1) < thisDays) {
                    break;
                } else {
                    daysCounter = thisDays;
                }
            } else {
                numDays = monthArr[currMonth - 1];
                daysCounter += parseInt(numDays)
            }

            if (daysCounter > (targetDays * -1)) {
                break;
            }

            currMonth = currMonth - 1;

            if (currMonth == 0) {
                currYear = currYear - 1;
                if ((currYear % 4) == 0) {
                    if ((currYear % 100) == 0 && (currYear % 400) != 0) { monthArr = nonleap; }
                    else { monthArr = leap; }
                }
                else { monthArr = nonleap; }
                currMonth = 12;
            }
        }

        t = this.getTime();
        t += (targetDays * 86400000);
        this.setTime(t)
        var thisDate = new Date(currYear, currMonth - 1, this.getDate())
        return thisDate;

    } else {

        var diffDays = monthArr[currMonth - 1] - thisDays;

        numDays = 0;
        var startedC = true;

        while (daysCounter < targetDays) {

            if (daysCounter == 0 && startedC == true) {
                monthDays = thisDays;
                startedC = false;
            } else {
                monthDays++;
                daysCounter++;

                if (monthDays > monthArr[currMonth - 1]) {
                    currMonth = currMonth + 1;
                    monthDays = 1;
                }

            }

            if (daysCounter > targetDays) {
                break;
            }

            if (currMonth == 13) {
                currYear = currYear + 1;
                if ((currYear % 4) == 0) {
                    if ((currYear % 100) == 0 && (currYear % 400) != 0) { monthArr = nonleap; }
                    else { monthArr = leap; }
                }
                else { monthArr = nonleap; }
                currMonth = 1;
            }
        }

        var thisDate = new Date(currYear, currMonth - 1, monthDays)
        return thisDate;
    }
}