from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QWidget, QSplitter, QHBoxLayout

from view.data import Data
from view.visualisation import Visualisation
from view.search import Search


class Gui(QWidget):

    def __init__(self):
        super().__init__()

        self.resize(1000, 650)
        self.setMinimumSize(1000, 650)
        # self.setMaximumSize(1000, 1000)
        self.setWindowTitle('Search Engine Results Pages')

        self.data = Data()
        self.visual = Visualisation()
        self.search = Search(self.data, self.visual)

        frame2_split = QSplitter(Qt.Horizontal)
        frame2_split.addWidget(self.data)
        frame2_split.addWidget(self.visual)
        frame2_split.setSizes([480, 480])

        split = QSplitter(Qt.Vertical)
        split.addWidget(self.search)
        split.addWidget(frame2_split)
        split.setSizes([120, 680])

        hbox = QHBoxLayout()
        hbox.addWidget(split)
        
        self.setLayout(hbox)
        self.show()