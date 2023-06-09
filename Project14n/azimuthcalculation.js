
class Point {
  constructor(x, y) {
    this.x = x;
    this.y = y;
  }
}

class Line {
  constructor(x1, y1, x2, y2) {
    this.point1 = new Point(x1, y1);
    this.point2 = new Point(x2, y2);
  }

  // Calculate the azimuth angle between the two endpoints of the line
  getAzimuth() {
    const y1Radians = this.point1.y * Math.PI / 180;
    const y2Radians = this.point2.y * Math.PI / 180;

    const deltaX = (this.point2.x - this.point1.x) * Math.PI / 180;

    const yComponent = Math.sin(deltaX) * Math.cos(y2Radians);
    const xComponent =
      Math.cos(y1Radians) * Math.sin(y2Radians) -
      Math.sin(y1Radians) * Math.cos(y2Radians) * Math.cos(deltaX);

    const theta = Math.atan2(yComponent, xComponent);
    const azimuthDegrees = ((theta / Math.PI * 180) + 360) % 360;
    return azimuthDegrees;
  }
}

// This function is called when the user clicks a button
function calculateAzimuth() {
  const form = document.getElementById("azimuthForm");

  // Get the x and y coordinates 
  const x1 = Number(form.elements["x1"].value);
  const y1 = Number(form.elements["y1"].value);
  const x2 = Number(form.elements["x2"].value);
  const y2 = Number(form.elements["y2"].value);

  const line = new Line(x1, y1, x2, y2);
  const azimuth = line.getAzimuth();

  // Display the calculated azimuth 
  const resultElement = document.getElementById("azimuthResult");
  resultElement.textContent = `The azimuth angle is: ${azimuth.toFixed(4)} degrees.`;
}