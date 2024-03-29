﻿function removeFromArray(arr, ele) {
    for (var i = arr.length - 1; i >= 0; i--) {
        if (arr[i] === ele)
            arr.splice(i, 1);
    }
}

function heuristic(a, b) {
    var d = dist(a.x, a.y, b.x, b.y); // Euclidean distance
    // var d = abs(a.x - b.x) + abs(a.y - b.y);
    return d;
}

var cols = 50;
var rows = 50;
var grid = new Array(cols);

var openSet = [];
var closedSet = [];
var start;
var end;
var w, h;
var path = [];

function Spot(x, y) {
    this.x = x;
    this.y = y;
    this.f = 0;
    this.g = 0;
    this.h = 0;
    this.neighbors = [];
    this.previous = undefined;
    this.isWall = false;

    if (random(1) < 0.3)
        this.isWall = true;

    this.show = function (color) {
        fill(color);
        if (this.isWall)
            fill(0);
        stroke(0);
        rect(this.x * w, this.y * h, w, h);
    };

    this.addNeighbors = function (grid) {
        var x = this.x;
        var y = this.y;

        if (x < cols - 1)
            this.neighbors.push(grid[x + 1][y]);
        if (x > 0)
            this.neighbors.push(grid[x - 1][y]);
        if (y < rows - 1)
            this.neighbors.push(grid[x][y + 1]);
        if (y > 0)
            this.neighbors.push(grid[x][y - 1]);
        if (x > 0 && y > 0)
            this.neighbors.push(grid[x - 1][y - 1]);
        if (x < cols - 1 && y > 0)
            this.neighbors.push(grid[x + 1][y - 1]);
        if (x > 0 && y < rows - 1)
            this.neighbors.push(grid[x - 1][y + 1]);
        if (x < cols - 1 && y < rows - 1)
            this.neighbors.push(grid[x + 1][y + 1]);
    };
}

function setup() {
    createCanvas(400, 400);
    console.log('A*');

    w = width / cols;
    h = height / rows;

    // Creating 2D array
    for (var i = 0; i < cols; i++) {
        grid[i] = new Array(rows);
    }

    for (var i = 0; i < cols; i++) {
        for (var j = 0; j < rows; j++)
            grid[i][j] = new Spot(i, j);
    }

    for (var i = 0; i < cols; i++) {
        for (var j = 0; j < rows; j++)
            grid[i][j].addNeighbors(grid);
    }

    start = grid[0][0];
    end = grid[cols - 1][rows - 1];
    start.isWall = false;
    end.isWall = false;

    openSet.push(start);

    console.log(grid);
}

function draw() {

    if (openSet.length > 0) {
        // Searching for solution
        var winner = 0;

        // Calculates lowest distance openSet element
        for (var i = 0; i < openSet.length; i++) {
            if (openSet[i].f < openSet[winner].f) {
                winner = i;
            }
        }

        var current = openSet[winner];

        if (current === end) {
            noLoop();
            console.log("Solution found");
        }

        removeFromArray(openSet, current); // Resets openSet
        closedSet.push(current); // Marks that currently calculated openSet winner element is calculated

        var neighbors = current.neighbors;

        // Calculates and sets g, h and f values for neighbors for the next iteration
        for (var i = 0; i < neighbors.length; i++) {
            var neighbor = neighbors[i];

            if (!closedSet.includes(neighbor) && !neighbor.isWall) {
                var tempG = current.g + 1;
                var isNewPath = false;

                if (openSet.includes(neighbor)) {
                    if (tempG < neighbor.g) {
                        neighbor.g = tempG;
                        isNewPath = true;
                    }
                } else {
                    neighbor.g = tempG;
                    isNewPath = true;
                    openSet.push(neighbor);
                }

                if (isNewPath) {
                    neighbor.h = heuristic(neighbor, end); // Calculates global distance to finish
                    neighbor.f = neighbor.g + neighbor.h;
                    neighbor.previous = current; // Sets the origin of the position that was before this one
                }
            }

        }

    } else {
        // No solution
        console.log("No solution");
        noLoop();
        return;
    }

    background(0);

    for (var i = 0; i < cols; i++) {
        for (var j = 0; j < rows; j++) {
            grid[i][j].show(color(255));
        }
    }

    for (var i = 0; i < closedSet.length; i++) {
        closedSet[i].show(color(255, 0, 0));
    }

    for (var i = 0; i < openSet.length; i++) {
        openSet[i].show(color(0, 255, 0));
    }

    // Find the path
    path = [];
    var temp = current;
    path.push(temp);
    while (temp.previous) {
        path.push(temp.previous);
        temp = temp.previous;
    }

    for (var i = 0; i < path.length; i++) {
        path[i].show(color(0, 0, 255));
    }

    noFill();
    stroke(255);
    beginShape();
    for (var i = 0; i < path.length; i++) {
        vertex(path[i].x * w + w / 2, path[i].y * h + h / 2);
    }
    endShape();
}