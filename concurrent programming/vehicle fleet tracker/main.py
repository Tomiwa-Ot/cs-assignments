#!/usr/bin/env python3

from PyQt5.QtWidgets import QApplication
from view import Gui


if __name__ == '__main__':
    app = QApplication([])
    gui = Gui()
    app.exec()