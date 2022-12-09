
import requests
from threading import Lock
from PyQt5 import QtGui, QtTest
from PyQt5.QtCore import Qt, QTimer
from PyQt5.QtWidgets import QWidget, QSplitter, QHBoxLayout, QPushButton
from model.vehicle import VehicleTracker
from view.location import Location
from view.map import Map
from view.resources import Resources



class Gui(QWidget):

    def __init__(self):
        super().__init__()
        res = requests.get('https://api.mapbox.com/directions/v5/mapbox/driving/3.384685%2C6.517622%3B3.397718%2C6.515656?alternatives=true&geometries=geojson&language=en&overview=full&steps=true&access_token=YOUR_MAPBOX_ACCESS_TOKEN_HERE')
        self.route1 = res.json()['routes'][0]['geometry']['coordinates']
        self.route2 = res.json()['routes'][1]['geometry']['coordinates']
        self.routes = [self.route1, self.route2]
        self.lock = Lock()
        self.tracker = VehicleTracker(self.route1[0][0], self.route1[0][1], self.lock)

        self.timers = [QTimer(), QTimer(), QTimer(), QTimer(), QTimer(), QTimer()]
        for i, timer in enumerate(self.timers):
            timer.setInterval(700)
            timer.timeout.connect(lambda i=i: self.move(i))
        self.index = [1] * 6

        self.resize(1500, 600)
        self.setMinimumSize(1450, 650)
        self.setMaximumSize(1450, 650)
        self.setWindowTitle('Tracking Vehicle Fleet')
        self.setWindowIcon(QtGui.QIcon(Resources.audi))

        self.start_button = QPushButton('Start', self)
        self.start_button.clicked.connect(self.start)
        self.start_button.setEnabled(False)
        
        horizontal_split = QSplitter(Qt.Horizontal)
        self.loc = Location(self.tracker)
        horizontal_split.addWidget(self.loc)
        self.map = Map(self.tracker, self.loc, self.start_button)
        horizontal_split.addWidget(self.map)
        horizontal_split.setSizes([350, 1100])

        vertical_split = QSplitter(Qt.Vertical)
        vertical_split.addWidget(horizontal_split)
        vertical_split.addWidget(self.start_button)
        vertical_split.setSizes([650, 50])

        hbox = QHBoxLayout()
        hbox.addWidget(vertical_split)
        
        self.setLayout(hbox)
        self.show()

    def start(self):
        for timer in self.timers:
            timer.start()
            QtTest.QTest.qWait(4000)

    def move(self, id):
        if self.index[id] == len(self.routes[id % 2]):
            self.timers[id].stop()
            self.index[id] = 0
            return
        self.map.move(id, str(self.routes[id % 2][self.index[id]][0]), str(self.routes[id % 2][self.index[id]][1]))
        self.index[id] += 1