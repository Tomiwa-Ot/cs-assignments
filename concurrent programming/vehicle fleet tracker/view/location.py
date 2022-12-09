from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QWidget
from PyQt5.QtGui import QFont, QPixmap, QPainter
from PyQt5.QtWidgets import QAbstractItemView
from PyQt5.QtWidgets import QFrame, QTableWidget, QTableWidgetItem, QHBoxLayout

from model.vehicle import VehicleTracker

class Location(QFrame):

    def __init__(self, tracker) -> None:
        super().__init__()
        self.tracker = tracker
        self.setFrameShape(QFrame.StyledPanel)

        font = QFont()
        font.setBold(True)
        font.setPixelSize(20)

        self.table = QTableWidget()
        self.table.setRowCount(7)
        self.table.setColumnCount(3)
        self.table.setRowHeight
        self.table.setEditTriggers(QAbstractItemView.NoEditTriggers)
        for i in range(1, 7):
            self.table.setRowHeight(i, 70)

        v = QTableWidgetItem('Vehicle')
        v.setFont(font)
        v.setTextAlignment(Qt.AlignHCenter)
        self.table.setItem(0, 0, v)

        x = QTableWidgetItem('x')
        x.setFont(font)
        x.setTextAlignment(Qt.AlignHCenter)
        self.table.setItem(0, 1, x)

        y = QTableWidgetItem('y')
        y.setFont(font)
        y.setTextAlignment(Qt.AlignHCenter)
        self.table.setItem(0, 2, y)

        for i in range(1, 7):
            self.table.setCellWidget(i, 0, ImageWidget(VehicleTracker.icons[i - 1], self.table.rowHeight(1)))
        
        for i in range(3):
            self.table.setColumnWidth(i, 90)
        
        for i in range(1, 7):
            self.update(i)

        hbox = QHBoxLayout()
        hbox.addWidget(self.table)

        self.setLayout(hbox)

    def update(self, i) -> None:
        x = QTableWidgetItem(f'{self.tracker.locations[i - 1].x}')
        x.setTextAlignment(Qt.AlignCenter)
        y = QTableWidgetItem(f'{self.tracker.locations[i - 1].y}')
        y.setTextAlignment(Qt.AlignCenter)
        self.table.setItem(i, 1, x)
        self.table.setItem(i, 2, y)


class ImageWidget(QWidget):

    def __init__(self, path, h) -> None:
        super().__init__()
        self.pic = QPixmap(path)
        self.h = h

    def paintEvent(self, event) -> None:
        painter = QPainter(self)
        painter.drawPixmap(0, 0, self.h, self.h, self.pic)