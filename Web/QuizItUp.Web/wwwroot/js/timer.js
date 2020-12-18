$("#start").click(function () {
    var minsInput = document.getElementById("minutes");

    var mins = null;

    if (minsInput) {
        mins = minsInput.value;
    }

    if (mins) {
        $('#timerElement').show();
    }

    let now = new Date($.now());
    let endTime = getEndDate(now, mins);
    initializeTime('timerElement', endTime);

    function getLeftTime(endtime) {
        var t = Date.parse(endtime) - Date.parse(new Date());
        var seconds = Math.floor((t / 1000) % 60);
        var minutes = Math.floor((t / 1000 / 60) % 60);

        return {
            'total': t,
            'minutes': minutes,
            'seconds': seconds
        };
    }

    function initializeTime(id, endtime) {
        var clock = document.getElementById(id);
        var minutesSpan = clock.querySelector('.minutes');
        var secondsSpan = clock.querySelector('.seconds');

        function updateClock() {
            var t = getLeftTime(endtime);

            minutesSpan.innerHTML = ('0' + t.minutes).slice(-2);
            secondsSpan.innerHTML = ('0' + t.seconds).slice(-2);

            if (t.total <= 0) {
                clearInterval(timeinterval);
                $(window).unbind('beforeunload');
                $('#submitResult').click();
            }
        }

        updateClock();
        var timeinterval = setInterval(updateClock, 1000);
    }

    function getEndDate(dt, minutes) {
        return new Date(dt.getTime() + minutes * 60000).toString();
    }
})
