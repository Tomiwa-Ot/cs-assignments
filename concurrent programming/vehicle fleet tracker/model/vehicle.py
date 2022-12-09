from model.point import Point
from view.resources import Resources


class VehicleTracker:

    icons = {
        0 : Resources.ambulance,
        1 : Resources.audi,
        2 : Resources.police,
        3 : Resources.taxi,
        4 : Resources.truck,
        5 : Resources.viper
    }

    def __init__(self, x, y, lock) -> None:
        self.lock = lock
        self.locations = {
            0 : Point(x, y),
            1 : Point(x, y),
            2 : Point(x, y),
            3 : Point(x, y),
            4 : Point(x, y),
            5 : Point(x, y)
        }

    def get_location(self, id) -> Point:
        location = None
        self.lock.acquire()
        location = self.locations[id]
        self.lock.release()
        return location

    def get_locations(self) -> dict:
        location = None
        self.lock.acquire()
        location = self.locations
        self.lock.release()
        return location

    def set_location(self, id, x, y) -> None:
        self.lock.acquire()
        self.locations[id].x = x
        self.locations[id].y = y
        self.lock.release()
