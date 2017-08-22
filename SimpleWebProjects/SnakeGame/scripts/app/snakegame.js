/// <reference path="../jquery-3.1.1.js" />


function snakeladder(elem_id, no_of_boxes) {
    this.target_elem_id = elem_id;
    this.no_of_boxes = no_of_boxes;
    this.no_of_users = 0;
    var that = this;
    this.users = [];
    this.current_user_id = 0;
    this.snakes = [{ start: 17, end: 7 }, { start: 54, end: 34 }, { start: 62, end: 19 }, { start: 64, end: 60 }, { start: 87, end: 24 }, { start: 93, end: 73 }, { start: 95, end: 75 }, { start: 99, end: 78 }];
    this.ladders = [{ start: 4, end: 14 }, { start: 9, end: 31 }, { start: 28, end: 84 }, { start: 20, end: 38 }, { start: 40, end: 59 }, { start: 63, end: 81 }, { start: 51, end: 67 }, { start: 71, end: 91 }];

    this.bindevents = function () {
        var parent_elem = $("#" + this.target_elem_id);
        parent_elem.find('.roll_btn').on("click", function () {
            that.throw_d(parent_elem);
        });
    }

    this.check_snaketrap_ladderjump = function (current_po) {
        var trapped = false;
        var ladder_point = false;
        var adjusted_po = current_po;
        var trap = that.snakes.filter(m => m.start == current_po);
        if (trap.length > 0) {
            trapped = true;
            adjusted_po = trap[0].end;
        }
        else {
            var ladder = that.ladders.filter(m => m.start == current_po);
            if (ladder.length > 0) {
                ladder_point = true;
                adjusted_po = ladder[0].end;
            }
        }

        return { snake_bite: trapped, ladder_jump: ladder_point, current_po: adjusted_po };
    }

    this.generate_rand_dice_val = function () {
        var r = parseInt(Math.round(Math.random().toPrecision(2) * 6));
        if (r == 0) {
            r = 1;
        }

        return r;
    }

    this.throw_d = function (parent_elem) {
        //var parent_elem = $("#" + this.target_elem_id);
        var r = that.generate_rand_dice_val();

        parent_elem.find('input').val(r);
        var c_user = {};
        $.each(that.users, function (i, v) {
            if (v.id == that.current_user_id) {
                c_user = v;
            }
        });
        console.log(r + "  " + c_user.p_n);
        var adjusted_po = parseInt(c_user.p_n + r);
        var trap_jump_check = that.check_snaketrap_ladderjump(adjusted_po);
        if (trap_jump_check.ladder_jump || trap_jump_check.snake_bite) {
            adjusted_po = trap_jump_check.current_po;
        }

        if (adjusted_po <= that.no_of_boxes) {
            parent_elem.find("#" + that.target_elem_id + "_" + (c_user.p_n)).find('.user_pos_indicator span[data-userid="' + that.current_user_id + '"]').remove();
            var curr_box = parent_elem.find("#" + that.target_elem_id + "_" + adjusted_po);
            curr_box.find(".user_pos_indicator").append('<span data-userid="' + that.current_user_id + '">' + that.current_user_id + "</span>");
            $(this).val("");
            c_user.p_n = adjusted_po;
            if (c_user.p_n == that.no_of_boxes) {
                that.users.find(m => m.id == that.current_user_id).active = false;
            }
        }
        that.set_current_user(parent_elem);

    }

    this.set_current_user = function (parent_elem) {
        var active_user_count = this.users.filter(m => m.active == true).length;

        parent_elem.find(".user_container li").removeClass("active_user");
        if (active_user_count == 0) {
            parent_elem.find('.roll_btn').attr("disabled", true);
        }
        else {
            this.set_current_userid(parent_elem);
            parent_elem.find("#U_" + that.target_elem_id + "_" + that.current_user_id).addClass("active_user");
        }
    }

    this.set_current_userid = function (parent_elem) {
        for (var i = that.current_user_id + 1; i < that.no_of_users + that.current_user_id; i++) {
            if (i > that.no_of_users) {
                if (that.users[i - that.no_of_users - 1].active) {
                    that.current_user_id = i - that.no_of_users;
                    return;
                }
            }
            else {
                if (that.users[i - 1].active) {
                    that.current_user_id = i;
                    return;
                }
            }
        }
    }

    this.load = function () {
        var boxes_html = "";
        this.no_of_users = this.users.length;
        var rows_count = Math.floor(this.no_of_boxes / 10);
        for (var i = rows_count; i > 0; i--) {

            if (i % 2 == 0) {
                for (var j = i * 10 ; j > (i * 10 - 10) ; j--) {
                    boxes_html += '<li class="box" id="' + this.target_elem_id + "_" + j + '"><div class="user_pos_indicator"></div><div class="box_val"><span>' + '</span></div>' + '</li>';
                }
            }
            else {
                for (var j = (i - 1) * 10 + 1 ; j <= (i - 1) * 10 + 10; j++) {
                    boxes_html += '<li class="box"id="' + this.target_elem_id + "_" + j + '"><div class="user_pos_indicator"></div><div class="box_val"><span>' + '</span></div>' + '</li>';
                }
            }


        }
        var elem = $('#' + this.target_elem_id);
        elem.append('<div style="clear:both"></div><ul class="game_container">' + boxes_html + '</ul>');

        elem.append("<div class='roll_container'><input type='text' class='roll_v_d'/><button class='roll_btn'>Roll</button></div>")
        var u_elem = "";

        $.each(this.users, function (i, v) {
            u_elem += '<li id="U_' + that.target_elem_id + "_" + v.id + '" class="usericon"><img src="' + v.user_image + '" /></li>';
        });

        elem.append("<ul class='user_container'>" + u_elem + "</ul>");

        this.current_user_id = 1;
        elem.find(".user_container li:eq(0)").addClass("active_user");
        this.bindevents();

    }
}