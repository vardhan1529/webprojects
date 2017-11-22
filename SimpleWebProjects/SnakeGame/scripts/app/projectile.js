/// <reference path="../jquery-3.1.1.js" />

jQuery.fn.extend({
    projectile: projectile
});

function projectile(props) {
    props = props || {};

    var p = function (width, height, parentElem, i_velocity, angle) {
        this.interval = { x: 0, y: 0 };
        this.max = { x: 0, y: 0 };
        this.dimensions = { w: width, h: height };
        this.coordinates = [];
        this.is_graph_plotted = false;
        this.context = null;
        this.inputs = { v: i_velocity || 0, a: angle || 0 };
        this.acceleration = 9.8;
        this.margin_width = 50;
        this.origin = { x: this.margin_width, y: height - this.margin_width };

        this.init = function () {
            var canvasElem = document.createElement('canvas');
            canvasElem.width = this.dimensions.w;
            canvasElem.height = this.dimensions.h;
            canvasElem.style.border = "1px solid black";
            canvasElem.onmousemove = this.showReading;
            parentElem.innerHTML = "";
            parentElem.append(canvasElem);
            this.context = canvasElem.getContext('2d');
            this.context.lineWidth = 3;
        }

        this.execute = function (inputs) {
            inputs = inputs || {};
            if (inputs.i_velocity) {
                this.inputs.v = inputs.i_velocity;
            }

            if (inputs.angle) {
                this.inputs.a = inputs.angle;
            }

            this.is_graph_plotted = false;
            this.coordinates = [{ x: 0, y: 0 }];
            this.calculateCoordinates();
            this.draw();
            this.is_graph_plotted = true;
        }

        this.calculateCoordinates = function () {
            var rad = 0.0174533 * this.inputs.a;
            this.max.x = (Math.pow(this.inputs.v, 2) * Math.sin(2 * rad)) / this.acceleration;
            this.max.y = (Math.pow(this.inputs.v, 2) * Math.sin(rad) * Math.sin(rad)) / (2 * this.acceleration);
            this.interval.x = this.max.x / (this.dimensions.w - this.margin_width);
            this.interval.y = this.max.y / (this.dimensions.h - this.margin_width);

            for (var x = 1; x < this.dimensions.w - this.margin_width; x++) {
                let y = ((x * this.interval.x) * Math.tan(rad) - this.acceleration * Math.pow((x * this.interval.x), 2) / (2 * Math.pow(this.inputs.v, 2) * Math.pow(Math.cos(rad), 2))) / this.interval.y;
                this.coordinates.push({ x: x, y: y });
            }
        }

        this.draw = function () {
            this.context.setTransform(1, 0, 0, 1, 0, 0);
            this.context.clearRect(0, 0, this.dimensions.w, this.dimensions.h);
            this.context.translate(this.origin.x, this.origin.y);

            //Draw X,Y Axis
            this.context.beginPath();
            this.context.moveTo(0, -this.origin.y);
            this.context.lineTo(0, 0);
            this.context.lineTo(this.dimensions.w - 50, 0);
            this.context.strokeStyle = 'skyblue';
            this.context.stroke();
            this.context.closePath();

            //Min,Max,Middle values for Axis

            this.context.beginPath();
            this.context.fillStyle = 'orange';
            this.context.font = "20px Ariel";
            this.context.fillText(this.max.y, -20, -this.origin.y + 20);
            this.context.fillText(this.max.y / 2, -20, -this.origin.y / 2);
            this.context.fillText("0,0", -20, 20);
            this.context.fillText(this.max.x / 2, (this.dimensions.w - this.origin.x) / 2, 20);
            this.context.fillText(this.max.x, this.dimensions.w - this.origin.x - 50, 20);
            this.context.stroke();
            this.context.closePath();


            this.context.strokeStyle = 'orange';
            this.context.beginPath();
            this.context.moveTo(this.coordinates[0].x, this.coordinates[0].y);
            for (var i = 1; i < this.coordinates.length - 1; i++) {
                this.context.lineTo(this.coordinates[i].x, -this.coordinates[i].y);
                this.context.stroke();
            }

            this.context.closePath();
        }

        this.load = function () {
            this.init();
            this.execute();
        }

        var that = this;
        this.showReading = function (e) {
            if (!that.is_graph_plotted) {
                return;
            }

            that.draw();
            var x = e.offsetX;
            var y = e.offsetY;
            for (var i = 0; i < 700; i++) {
                var c = that.coordinates[i];
                if (c.x <= x - that.origin.x + 2 && c.x >= x - that.origin.x - 2 && c.y >= that.origin.y - y - 2 && c.y <= that.origin.y - y + 2) {
                    that.context.beginPath();
                    that.context.fillStyle = 'red';
                    that.context.font = "20px Ariel";
                    that.context.fillText(Math.round(c.x * that.interval.x) + " , " + Math.round(c.y * that.interval.y), c.x, -c.y);
                    that.context.stroke();
                    that.context.closePath();
                    break;
                }
            }
        }
    };

    return new p(props.width || 750, props.height || 550, $(this), props.i_velocity, props.angle);
}